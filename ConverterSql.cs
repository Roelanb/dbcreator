using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;

public partial class Converter
{
    private void GenerateDatabaseObjectsFiles(string databaseFolder, ObjectToMap o)
    {
        var tableScript = GenerateTableScript(o, o.Columns, o.PrimaryKey);

        var createSp = GenerateCreateSp(o, o.Columns, o.PrimaryKey);
        var updateSp = GenerateUpdateSp(o, o.Columns, o.PrimaryKey);
        var deleteSp = GenerateDeleteSp(o, o.Columns, o.PrimaryKey);
        var readSp = GenerateReadSp(o, o.Columns, o.PrimaryKey);

        var tableScriptFilename = $"table_{o.TableName}.sql";
        var createSpFilename = $"csp_dlm_{o.TableName}_create.sql";
        var updateSpFilename = $"csp_dlm_{o.TableName}_update.sql";
        var deleteSpFilename = $"csp_dlm_{o.TableName}_delete.sql";
        var readSpFilename = $"csp_dlm_{o.TableName}_read.sql";

        File.WriteAllText(databaseFolder + "/" + tableScriptFilename, tableScript);
        File.WriteAllText(databaseFolder + "/" + createSpFilename, createSp);
        File.WriteAllText(databaseFolder + "/" + updateSpFilename, updateSp);
        File.WriteAllText(databaseFolder + "/" + deleteSpFilename, deleteSp);
        File.WriteAllText(databaseFolder + "/" + readSpFilename, readSp);

        var sqlScriptActions = new SqlScriptActions();

        Console.WriteLine($"Executing SQL scripts for {o.TableName}");
        sqlScriptActions.ExecuteScript(o.ConnectionString, databaseFolder + "/" + tableScriptFilename);
        sqlScriptActions.ExecuteScript(o.ConnectionString, databaseFolder + "/" + createSpFilename);
        sqlScriptActions.ExecuteScript(o.ConnectionString, databaseFolder + "/" + updateSpFilename);
        sqlScriptActions.ExecuteScript(o.ConnectionString, databaseFolder + "/" + deleteSpFilename);
        sqlScriptActions.ExecuteScript(o.ConnectionString, databaseFolder + "/" + readSpFilename);

    }

    private StringBuilder GenerateParameters(ObjectToMap o,
                            List<ColumnDefinition> columns,
                            string primaryKey)
    {
        var sb = new StringBuilder();
        foreach (var column in columns)
        {
            if (column.SqlType == "nvarchar" || column.SqlType == "varchar")
            {
                sb.AppendLine($"    @{column.ColumnName} {column.SqlType}({column.MaxLength}),");
            }
            else
            {
                sb.AppendLine($"    @{column.ColumnName} {column.SqlType},");
            }

        }
        sb.Remove(sb.Length - 3, 3);
        sb.AppendLine();
        return sb;
    }

    private string GenerateTableScript(ObjectToMap o,
                            List<ColumnDefinition> columns,
                            string primaryKey)
    {
        Console.WriteLine($"Generating SQL table script for {o.TableName}");


        var sb = new StringBuilder();
        sb.AppendLine($"IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[{o.TableName}]') AND type in (N'U'))  return;");

        sb.Append(GenerateTableScript("CREATE", o, columns, primaryKey));
              
    

        return sb.ToString();
    }

    private StringBuilder GenerateTableScript(string action,ObjectToMap o,
                            List<ColumnDefinition> columns,
                            string primaryKey)
    {
        Console.WriteLine($"Generating SQL table script for {o.TableName}");


        var sb = new StringBuilder();
        
        sb.AppendLine($"{action} TABLE [dbo].[{o.TableName}]");
        sb.AppendLine("(");
        foreach (var column in columns)
        {
            if (column.SqlType == "nvarchar" || column.SqlType == "varchar")
            {
                sb.AppendLine($"    [{column.ColumnName}] {column.SqlType}({column.MaxLength}) {(column.IsNullable ? "NULL" : "NOT NULL")},");
            }
            else
            {
                sb.AppendLine($"    [{column.ColumnName}] {column.SqlType} {(column.IsNullable ? "NULL" : "NOT NULL")},");
            }
        }
        sb.AppendLine($"    CONSTRAINT [PK_{o.TableName}] PRIMARY KEY CLUSTERED ([{primaryKey}] ASC)");
        sb.AppendLine(")");
        sb.AppendLine("GO");
        sb.AppendLine($"GRANT SELECT, INSERT, UPDATE, DELETE ON [dbo].[{o.TableName}] TO [app_datawriters]");
        sb.AppendLine("GO");
        sb.AppendLine($"GRANT SELECT ON [dbo].[{o.TableName}] TO [app_datareaders]");
        sb.AppendLine("GO");

        return sb;
    }

    private string GenerateCreateSp(ObjectToMap o,
                            List<ColumnDefinition> columns,
                            string primaryKey,
                            string createOrAlter = "CREATE")
    {
        Console.WriteLine($"Generating SQL create stored procedure for {o.TableName}");

        var sb = new StringBuilder();

        // DROP PROCEDURE [dbo].[csp_dlm_Applications_create]
        sb.AppendLine($"IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[csp_dlm_{o.TableName}_create]') AND type in (N'P', N'PC'))");
        sb.AppendLine($"DROP PROCEDURE [dbo].[csp_dlm_{o.TableName}_create]");
        sb.AppendLine("GO");

        sb.AppendLine($"{createOrAlter} PROCEDURE [dbo].[csp_dlm_{o.TableName}_create]");

        // add parameters
        sb.Append(GenerateParameters(o, columns, primaryKey));

        sb.AppendLine("AS");
        sb.AppendLine("BEGIN");
        sb.AppendLine($"    SET NOCOUNT ON;");

        sb.AppendLine($"    INSERT INTO {o.TableName} (");
        foreach (var column in columns)
        {
            sb.Append($"[{column.ColumnName}],");
        }
        sb.Remove(sb.Length - 1, 1);
        sb.AppendLine(")");

        sb.AppendLine("    VALUES (");
        foreach (var column in columns)
        {
            sb.AppendLine($"        @{column.ColumnName},");
        }
        sb.Remove(sb.Length - 3, 3);
        sb.AppendLine();
        sb.AppendLine("    );");

        sb.AppendLine("    SELECT");
        foreach (var column in columns)
        {
            sb.AppendLine($"        [{column.ColumnName}],");
        }
        sb.Remove(sb.Length - 3, 3);
        sb.AppendLine();
        sb.AppendLine("    FROM");
        sb.AppendLine($"        {o.TableName}");
        sb.AppendLine("    WHERE");
        sb.AppendLine($"        [{primaryKey}]= @{primaryKey}");


        sb.AppendLine("END");

        sb.AppendLine("GO");
        sb.AppendLine($"GRANT EXECUTE ON [dbo].[csp_dlm_{o.TableName}_create] TO [app_datawriters]");
        sb.AppendLine("GO");

        return sb.ToString();

    }

    private string GenerateUpdateSp(ObjectToMap o,
                            List<ColumnDefinition> columns,
                            string primaryKey,
                            string createOrAlter = "CREATE")
    {
        Console.WriteLine($"Generating SQL update stored procedure for {o.TableName}");

        var sb = new StringBuilder();

        sb.AppendLine($"IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[csp_dlm_{o.TableName}_update]') AND type in (N'P', N'PC'))");
        sb.AppendLine($"DROP PROCEDURE [dbo].[csp_dlm_{o.TableName}_update]");
        sb.AppendLine("GO");

        sb.AppendLine($"{createOrAlter} PROCEDURE [dbo].[csp_dlm_{o.TableName}_update]");
        // add parameters
        sb.Append(GenerateParameters(o, columns, primaryKey));

        sb.AppendLine("AS");
        sb.AppendLine("BEGIN");
        sb.AppendLine($"    SET NOCOUNT ON;");

        sb.AppendLine($"    UPDATE {o.TableName} SET");
        foreach (var column in columns)
        {
            sb.AppendLine($"        [{column.ColumnName}] = @{column.ColumnName},");
        }
        sb.Remove(sb.Length - 3, 3);
        sb.AppendLine();
        sb.AppendLine("    WHERE");
        sb.AppendLine($"        [{primaryKey}] = @{primaryKey}");

        sb.AppendLine();

        sb.AppendLine("    SELECT");
        foreach (var column in columns)
        {
            sb.AppendLine($"        [{column.ColumnName}],");
        }
        sb.Remove(sb.Length - 3, 3);
        sb.AppendLine();
        sb.AppendLine("    FROM");
        sb.AppendLine($"        {o.TableName}");
        sb.AppendLine("    WHERE");
        sb.AppendLine($"        [{primaryKey}] = @{primaryKey}");

        sb.AppendLine("END");
        
        sb.AppendLine("GO");
        sb.AppendLine($"GRANT EXECUTE ON [dbo].[csp_dlm_{o.TableName}_update] TO [app_datawriters]");
        sb.AppendLine("GO");
        
        return sb.ToString();
    }

    private string GenerateDeleteSp(ObjectToMap o,
                            List<ColumnDefinition> columns,
                            string primaryKey,
                            string createOrAlter = "CREATE")
    {
        Console.WriteLine($"Generating SQL delete stored procedure for {o.TableName}");

        var sb = new StringBuilder();

        sb.AppendLine($"IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[csp_dlm_{o.TableName}_delete]') AND type in (N'P', N'PC'))");
        sb.AppendLine($"DROP PROCEDURE [dbo].[csp_dlm_{o.TableName}_delete]");
        sb.AppendLine("GO");


        sb.AppendLine($"{createOrAlter} PROCEDURE [dbo].[csp_dlm_{o.TableName}_delete]");
        var keyColumn = columns.Find(x => x.ColumnName == primaryKey);

        if (keyColumn.SqlType == "nvarchar" || keyColumn.SqlType == "varchar")
        {
            sb.AppendLine($"    @{keyColumn.ColumnName} {keyColumn.SqlType}({keyColumn.MaxLength})");
        }
        else
        {
            sb.AppendLine($"    @{keyColumn.ColumnName} {keyColumn.SqlType}");
        }

        sb.AppendLine("AS");
        sb.AppendLine("BEGIN");
        sb.AppendLine($"    SET NOCOUNT ON;");

        sb.AppendLine($"    DELETE FROM {o.TableName}");
        sb.AppendLine("    WHERE");
        sb.AppendLine($"        [{primaryKey}] = @{primaryKey}");

        sb.AppendLine("END");
        
        sb.AppendLine("GO");
        sb.AppendLine($"GRANT EXECUTE ON [dbo].[csp_dlm_{o.TableName}_delete] TO [app_datawriters]");
        sb.AppendLine("GO");
        
        return sb.ToString();
    }

    private string GenerateReadSp(ObjectToMap o,
                            List<ColumnDefinition> columns,
                            string primaryKey,
                            string createOrAlter = "CREATE")
    {
        Console.WriteLine($"Generating SQL read stored procedure for {o.TableName}");

        var sb = new StringBuilder();

        sb.AppendLine($"IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[csp_dlm_{o.TableName}_read]') AND type in (N'P', N'PC'))");
        sb.AppendLine($"DROP PROCEDURE [dbo].[csp_dlm_{o.TableName}_read]");
        sb.AppendLine("GO");

        sb.AppendLine($"{createOrAlter} PROCEDURE [dbo].[csp_dlm_{o.TableName}_read]");
        var keyColumn = columns.Find(x => x.ColumnName == primaryKey);

        if (keyColumn.SqlType == "nvarchar" || keyColumn.SqlType == "varchar")
        {
            sb.AppendLine($"    @{keyColumn.ColumnName} {keyColumn.SqlType}({keyColumn.MaxLength}) = null");
        }
        else
        {
            sb.AppendLine($"    @{keyColumn.ColumnName} {keyColumn.SqlType} = null");
        }
        sb.AppendLine("AS");
        sb.AppendLine("BEGIN");
        sb.AppendLine($"    SET NOCOUNT ON;");

        sb.AppendLine($"    SELECT");
        foreach (var column in columns)
        {
            sb.AppendLine($"        [{column.ColumnName}],");
        }
        sb.Remove(sb.Length - 3, 3);
        sb.AppendLine();
        sb.AppendLine("    FROM");
        sb.AppendLine($"        {o.TableName}");
        sb.AppendLine("    WHERE");
        sb.AppendLine($"       ( [{primaryKey}]= @{primaryKey} OR @{primaryKey} IS NULL)");

        sb.AppendLine("END");
        
        sb.AppendLine("GO");
        sb.AppendLine($"GRANT EXECUTE ON [dbo].[csp_dlm_{o.TableName}_read] TO [app_datawriters]");
        sb.AppendLine("GO");
        sb.AppendLine($"GRANT EXECUTE ON [dbo].[csp_dlm_{o.TableName}_read] TO [app_datareaders]");
        sb.AppendLine("GO");
        
        
        return sb.ToString();
    }
}