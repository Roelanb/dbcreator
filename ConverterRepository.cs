using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;

public partial class Converter
{

    public string GenerateRepositoryClassString(ObjectToMap o)
    {
        Console.WriteLine($"Generating repository class for {o.TableName}");

        var sb = new StringBuilder();
    
        var primaryKeyType = o.Columns.Find(x => x.ColumnName == o.PrimaryKey).DataTypeToCSharpType();  

        sb.AppendLine("using System.Data;");
        sb.AppendLine("using Dapper;");
        sb.AppendLine($"using DataService.Config.Features.{o.FolderLevel1}.Entities;");
        sb.AppendLine($"using DataService.Shared.Configuration;");
        sb.AppendLine("using DataService.Shared.Entities;");
        sb.AppendLine("using Microsoft.Data.SqlClient;");
        sb.AppendLine("");
        sb.AppendLine($"namespace {o.NameSpace}.Shared;");

        sb.AppendLine("");
        sb.AppendLine($"public class {o.TableName}Repository : I{o.TableName}Repository");
        sb.AppendLine("{");
        sb.AppendLine("    private readonly ConnectionStringProvider _connectionStringProvider;");
        sb.AppendLine("");
        sb.AppendLine($"    public {o.TableName}Repository(ConnectionStringProvider connectionStringProvider)");
        sb.AppendLine("    {");
        sb.AppendLine("        _connectionStringProvider = connectionStringProvider;");
        sb.AppendLine("    }");
        sb.AppendLine("");

        
        sb.AppendLine($"    public async Task<QueryResult<IEnumerable<{o.EntityName}>>> ReadAsync({primaryKeyType}? {ConvertStringToCamelCase(o.PrimaryKey)})");
        sb.AppendLine("    {");
        sb.AppendLine("        try");
        sb.AppendLine("        {");
        sb.AppendLine($"            var result = new QueryResult<IEnumerable<{o.EntityName}>>();");
        sb.AppendLine("");
        sb.AppendLine($"            var connectionString = _connectionStringProvider.GetConnectionStrings(ConnectionStringKeys.MES).First();");
        sb.AppendLine("            using IDbConnection connection = new SqlConnection(connectionString.Value);");
        sb.AppendLine("            connection.Open();");
        sb.AppendLine("            var parameters = new DynamicParameters();");
        sb.AppendLine($@"             if ({ConvertStringToCamelCase(o.PrimaryKey)} != null) parameters.Add(""{o.PrimaryKey}"", {ConvertStringToCamelCase(o.PrimaryKey)}, DbType.String);");
        sb.AppendLine("");
        sb.AppendLine("");
        sb.AppendLine($"            result.Data = await connection.QueryAsync<{o.EntityName}>(\"csp_dlm_{o.TableName.ToLower()}_read\", parameters, commandType: CommandType.StoredProcedure);");
        sb.AppendLine("");
        sb.AppendLine("");
        sb.AppendLine("            return result;");
        sb.AppendLine("        }");
        sb.AppendLine("        catch (Exception ex)");
        sb.AppendLine("        {");
        sb.AppendLine("");
        sb.AppendLine($"            return new QueryResult<IEnumerable<{o.EntityName}>>(ex.Message);");
        sb.AppendLine("        }");
        sb.AppendLine("    }");
        sb.AppendLine("");
        sb.AppendLine($"public async Task<QueryResult<IEnumerable<{o.EntityName}>>> CreateAsync({o.EntityName} {ConvertStringToCamelCase(o.EntityName)})");
        sb.AppendLine("");
        sb.AppendLine("    {");
        sb.AppendLine("        try");
        sb.AppendLine("        {");
        sb.AppendLine($"            var result = new QueryResult<IEnumerable<{o.EntityName}>>();");   
        sb.AppendLine("");
        sb.AppendLine($"            var connectionString = _connectionStringProvider.GetConnectionStrings(ConnectionStringKeys.MES).First();");
        sb.AppendLine("            using IDbConnection connection = new SqlConnection(connectionString.Value);");
        sb.AppendLine("            connection.Open();");
        sb.AppendLine("            //Set up DynamicParameters object to pass parameters  ");
        sb.AppendLine("            var parameters = new DynamicParameters();");
        foreach (var column in o.Columns)
        {
            sb.AppendLine($"            parameters.Add(\"{column.ColumnName}\", {ConvertStringToCamelCase(o.EntityName)}.{column.ColumnName}, DbType.{column.DbType});");
        }
        sb.AppendLine("");
        sb.AppendLine("            //Execute stored procedure and map the returned result to a Customer object  ");
        sb.AppendLine($"            result.Data = await connection.QueryAsync<{o.EntityName}>(\"csp_dlm_{o.TableName.ToLower()}_create\", parameters, commandType: CommandType.StoredProcedure);");
        sb.AppendLine("");
        sb.AppendLine("            return result;");
        sb.AppendLine("        }");
        sb.AppendLine("        catch (Exception ex)");
        sb.AppendLine("        {");
        sb.AppendLine("");
        sb.AppendLine($"            return new QueryResult<IEnumerable<{o.EntityName}>>(ex.Message);");
        sb.AppendLine("        }");
        sb.AppendLine("    }");
        sb.AppendLine("");
        sb.AppendLine($"public async Task<QueryResult<IEnumerable<{o.EntityName}>>> UpdateAsync({o.EntityName} {ConvertStringToCamelCase(o.EntityName)})");
        sb.AppendLine("");
        sb.AppendLine("    {");
        sb.AppendLine("        try");
        sb.AppendLine("        {");
        sb.AppendLine($"            var result = new QueryResult<IEnumerable<{o.EntityName}>>();");
        sb.AppendLine("");
        sb.AppendLine($"            var connectionString = _connectionStringProvider.GetConnectionStrings(ConnectionStringKeys.MES).First();");
        sb.AppendLine("            using IDbConnection connection = new SqlConnection(connectionString.Value);");
        sb.AppendLine("            connection.Open();");
        sb.AppendLine("            //Set up DynamicParameters object to pass parameters  ");
        sb.AppendLine("            var parameters = new DynamicParameters();");
        foreach (var column in o.Columns)
        {
            sb.AppendLine($"            parameters.Add(\"{column.ColumnName}\", {ConvertStringToCamelCase(o.EntityName)}.{column.ColumnName}, DbType.{column.DbType});");
        }
        sb.AppendLine("");
        sb.AppendLine("            //Execute stored procedure and map the returned result to a Customer object  ");
        sb.AppendLine($"            result.Data = await connection.QueryAsync<{o.EntityName}>(\"csp_dlm_{o.TableName.ToLower()}_update\", parameters, commandType: CommandType.StoredProcedure);");
        sb.AppendLine("");
        sb.AppendLine("            return result;");
        sb.AppendLine("        }");
        sb.AppendLine("        catch (Exception ex)");
        sb.AppendLine("        {");
        sb.AppendLine("");
        sb.AppendLine($"            return new QueryResult<IEnumerable<{o.EntityName}>>(ex.Message);");
        sb.AppendLine("        }");
        sb.AppendLine("    }");
        sb.AppendLine("");
        sb.AppendLine($"public async Task<QueryResult<IEnumerable<{o.EntityName}>>> DeleteAsync({primaryKeyType} {ConvertStringToCamelCase(o.PrimaryKey)})");
        sb.AppendLine("");
        sb.AppendLine("    {");
        sb.AppendLine("        try");
        sb.AppendLine("        {");
        sb.AppendLine($"            var result = new QueryResult<IEnumerable<{o.EntityName}>>();");
        sb.AppendLine("");
        sb.AppendLine($"            var connectionString = _connectionStringProvider.GetConnectionStrings(ConnectionStringKeys.MES).First();");
        sb.AppendLine("            using IDbConnection connection = new SqlConnection(connectionString.Value);");
        sb.AppendLine("            connection.Open();");
        sb.AppendLine("            //Set up DynamicParameters object to pass parameters  ");
        sb.AppendLine("            var parameters = new DynamicParameters();");
        sb.AppendLine($"            parameters.Add(\"{o.PrimaryKey}\", {ConvertStringToCamelCase(o.PrimaryKey)}, DbType.{o.Columns.Find(x => x.ColumnName == o.PrimaryKey).DbType});");
        sb.AppendLine("");
        sb.AppendLine("            //Execute stored procedure and map the returned result to a Customer object  ");
        sb.AppendLine($"            result.Data = await connection.QueryAsync<{o.EntityName}>(\"csp_dlm_{o.TableName.ToLower()}_delete\", parameters, commandType: CommandType.StoredProcedure);");
        sb.AppendLine("");
        sb.AppendLine("            return result;");
        sb.AppendLine("        }");
        sb.AppendLine("        catch (Exception ex)");
        sb.AppendLine("        {");
        sb.AppendLine("");
        sb.AppendLine($"            return new QueryResult<IEnumerable<{o.EntityName}>>(ex.Message);");
        sb.AppendLine("        }");
        sb.AppendLine("    }");
        sb.AppendLine("}");
        sb.AppendLine("");



        return sb.ToString();
    }
    
    public string GenerateIRepositoryClassString(ObjectToMap o)
    {
        Console.WriteLine($"Generating repository interface for {o.TableName}");

        var keyFieldName = o.Columns.Find(x => x.ColumnName == o.PrimaryKey).ColumnName;

        var sb = new StringBuilder();
    

        sb.AppendLine($"using DataService.Config.Features.{o.FolderLevel1}.Entities;");
        sb.AppendLine("using DataService.Shared.Entities;");
        sb.AppendLine("");
        sb.AppendLine($"namespace {o.NameSpace}.Shared;");

        sb.AppendLine("");
        sb.AppendLine($"public interface I{o.TableName}Repository");
        sb.AppendLine("{");
        
        var primaryKeyType = o.Columns.Find(x => x.ColumnName == o.PrimaryKey).DataTypeToCSharpType();  

        sb.AppendLine($"    Task<QueryResult<IEnumerable<{o.EntityName}>>> ReadAsync({primaryKeyType}? {ConvertStringToCamelCase(keyFieldName)});");
        sb.AppendLine($"    Task<QueryResult<IEnumerable<{o.EntityName}>>> CreateAsync({o.EntityName} {ConvertStringToCamelCase(o.EntityName)});");
        sb.AppendLine($"    Task<QueryResult<IEnumerable<{o.EntityName}>>> UpdateAsync({o.EntityName} {ConvertStringToCamelCase(o.EntityName)});");
        sb.AppendLine($"    Task<QueryResult<IEnumerable<{o.EntityName}>>> DeleteAsync({primaryKeyType} {ConvertStringToCamelCase(keyFieldName)});");
        sb.AppendLine("}");
        sb.AppendLine("");



        return sb.ToString();
    }


}