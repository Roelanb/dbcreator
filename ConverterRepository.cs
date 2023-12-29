using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;

public partial class Converter
{

    private string GenerateRepositoryClassString(string ns, string tableName, string entityName, 
            List<ColumnDefinition> columns, string primaryKey)
    {
        Console.WriteLine($"Generating repository class for {tableName}");

        var sb = new StringBuilder();
    
        sb.AppendLine("using System.Data;");
        sb.AppendLine("using Dapper;");
        sb.AppendLine("using DataService.Config.Features.Translations.Entities;");
        sb.AppendLine("using DataService.Shared.Configuration;");
        sb.AppendLine("using DataService.Shared.Entities;");
        sb.AppendLine("using Microsoft.Data.SqlClient;");
        sb.AppendLine("");
        sb.AppendLine($"namespace {ns}.Shared;");

        sb.AppendLine("");
        sb.AppendLine($"public class {tableName}Repository : I{tableName}Repository");
        sb.AppendLine("{");
        sb.AppendLine("    private readonly ConnectionStringProvider _connectionStringProvider;");
        sb.AppendLine("");
        sb.AppendLine($"    public {tableName}Repository(ConnectionStringProvider connectionStringProvider)");
        sb.AppendLine("    {");
        sb.AppendLine("        _connectionStringProvider = connectionStringProvider;");
        sb.AppendLine("    }");
        sb.AppendLine("");
        sb.AppendLine($"    public async Task<QueryResult<IEnumerable<{entityName}>>> ReadAsync(string? {ConvertStringToCamelCase(primaryKey)})");
        sb.AppendLine("    {");
        sb.AppendLine("        try");
        sb.AppendLine("        {");
        sb.AppendLine($"            var result = new QueryResult<IEnumerable<{entityName}>>();");
        sb.AppendLine("");
        sb.AppendLine($"            var connectionString = _connectionStringProvider.GetConnectionStrings(ConnectionStringKeys.MES).First();");
        sb.AppendLine("            using IDbConnection connection = new SqlConnection(connectionString.Value);");
        sb.AppendLine("            connection.Open();");
        sb.AppendLine("            var parameters = new DynamicParameters();");
        sb.AppendLine($@"             if ({ConvertStringToCamelCase(primaryKey)} != null) parameters.Add(""{primaryKey}"", {ConvertStringToCamelCase(primaryKey)}, DbType.String);");
        sb.AppendLine("");
        sb.AppendLine("");
        sb.AppendLine($"            result.Data = await connection.QueryAsync<{entityName}>(\"csp_dlm_{tableName.ToLower()}_read\", parameters, commandType: CommandType.StoredProcedure);");
        sb.AppendLine("");
        sb.AppendLine("");
        sb.AppendLine("            return result;");
        sb.AppendLine("        }");
        sb.AppendLine("        catch (Exception ex)");
        sb.AppendLine("        {");
        sb.AppendLine("");
        sb.AppendLine($"            return new QueryResult<IEnumerable<{entityName}>>(ex.Message);");
        sb.AppendLine("        }");
        sb.AppendLine("    }");
        sb.AppendLine("");
        sb.AppendLine($"public async Task<QueryResult<IEnumerable<{entityName}>>> CreateAsync({entityName} {ConvertStringToCamelCase(entityName)})");
        sb.AppendLine("");
        sb.AppendLine("    {");
        sb.AppendLine("        try");
        sb.AppendLine("        {");
        sb.AppendLine($"            var result = new QueryResult<IEnumerable<{entityName}>>();");   
        sb.AppendLine("");
        sb.AppendLine($"            var connectionString = _connectionStringProvider.GetConnectionStrings(ConnectionStringKeys.MES).First();");
        sb.AppendLine("            using IDbConnection connection = new SqlConnection(connectionString.Value);");
        sb.AppendLine("            connection.Open();");
        sb.AppendLine("            //Set up DynamicParameters object to pass parameters  ");
        sb.AppendLine("            var parameters = new DynamicParameters();");
        foreach (var column in columns)
        {
            sb.AppendLine($"            parameters.Add(\"{column.ColumnName}\", {ConvertStringToCamelCase(entityName)}.{column.ColumnName}, DbType.{column.DbType});");
        }
        sb.AppendLine("");
        sb.AppendLine("            //Execute stored procedure and map the returned result to a Customer object  ");
        sb.AppendLine($"            result.Data = await connection.QueryAsync<{entityName}>(\"csp_dlm_{tableName.ToLower()}_create\", parameters, commandType: CommandType.StoredProcedure);");
        sb.AppendLine("");
        sb.AppendLine("            return result;");
        sb.AppendLine("        }");
        sb.AppendLine("        catch (Exception ex)");
        sb.AppendLine("        {");
        sb.AppendLine("");
        sb.AppendLine($"            return new QueryResult<IEnumerable<{entityName}>>(ex.Message);");
        sb.AppendLine("        }");
        sb.AppendLine("    }");
        sb.AppendLine("");
        sb.AppendLine($"public async Task<QueryResult<IEnumerable<{entityName}>>> UpdateAsync({entityName} {ConvertStringToCamelCase(entityName)})");
        sb.AppendLine("");
        sb.AppendLine("    {");
        sb.AppendLine("        try");
        sb.AppendLine("        {");
        sb.AppendLine($"            var result = new QueryResult<IEnumerable<{entityName}>>();");
        sb.AppendLine("");
        sb.AppendLine($"            var connectionString = _connectionStringProvider.GetConnectionStrings(ConnectionStringKeys.MES).First();");
        sb.AppendLine("            using IDbConnection connection = new SqlConnection(connectionString.Value);");
        sb.AppendLine("            connection.Open();");
        sb.AppendLine("            //Set up DynamicParameters object to pass parameters  ");
        sb.AppendLine("            var parameters = new DynamicParameters();");
        foreach (var column in columns)
        {
            sb.AppendLine($"            parameters.Add(\"{column.ColumnName}\", {ConvertStringToCamelCase(entityName)}.{column.ColumnName}, DbType.{column.DbType});");
        }
        sb.AppendLine("");
        sb.AppendLine("            //Execute stored procedure and map the returned result to a Customer object  ");
        sb.AppendLine($"            result.Data = await connection.QueryAsync<{entityName}>(\"csp_dlm_{tableName.ToLower()}_update\", parameters, commandType: CommandType.StoredProcedure);");
        sb.AppendLine("");
        sb.AppendLine("            return result;");
        sb.AppendLine("        }");
        sb.AppendLine("        catch (Exception ex)");
        sb.AppendLine("        {");
        sb.AppendLine("");
        sb.AppendLine($"            return new QueryResult<IEnumerable<{entityName}>>(ex.Message);");
        sb.AppendLine("        }");
        sb.AppendLine("    }");
        sb.AppendLine("");
        sb.AppendLine($"public async Task<QueryResult<IEnumerable<{entityName}>>> DeleteAsync(string {ConvertStringToCamelCase(primaryKey)})");
        sb.AppendLine("");
        sb.AppendLine("    {");
        sb.AppendLine("        try");
        sb.AppendLine("        {");
        sb.AppendLine($"            var result = new QueryResult<IEnumerable<{entityName}>>();");
        sb.AppendLine("");
        sb.AppendLine($"            var connectionString = _connectionStringProvider.GetConnectionStrings(ConnectionStringKeys.MES).First();");
        sb.AppendLine("            using IDbConnection connection = new SqlConnection(connectionString.Value);");
        sb.AppendLine("            connection.Open();");
        sb.AppendLine("            //Set up DynamicParameters object to pass parameters  ");
        sb.AppendLine("            var parameters = new DynamicParameters();");
        sb.AppendLine($"            parameters.Add(\"{primaryKey}\", {ConvertStringToCamelCase(primaryKey)}, DbType.{columns.Find(x => x.ColumnName == primaryKey).DbType});");
        sb.AppendLine("");
        sb.AppendLine("            //Execute stored procedure and map the returned result to a Customer object  ");
        sb.AppendLine($"            result.Data = await connection.QueryAsync<{entityName}>(\"csp_dlm_{tableName.ToLower()}_delete\", parameters, commandType: CommandType.StoredProcedure);");
        sb.AppendLine("");
        sb.AppendLine("            return result;");
        sb.AppendLine("        }");
        sb.AppendLine("        catch (Exception ex)");
        sb.AppendLine("        {");
        sb.AppendLine("");
        sb.AppendLine($"            return new QueryResult<IEnumerable<{entityName}>>(ex.Message);");
        sb.AppendLine("        }");
        sb.AppendLine("    }");
        sb.AppendLine("}");
        sb.AppendLine("");



        return sb.ToString();
    }
    
    private string GenerateIRepositoryClassString(string ns, string tableName, string entityName, 
                    List<ColumnDefinition> columns, string primaryKey)
    {
        Console.WriteLine($"Generating repository interface for {tableName}");

        var keyFieldName = columns.Find(x => x.ColumnName == primaryKey).ColumnName;

        var sb = new StringBuilder();
    

        sb.AppendLine("using DataService.Config.Features.Translations.Entities;");
        sb.AppendLine("using DataService.Shared.Entities;");
        sb.AppendLine("");
        sb.AppendLine($"namespace {ns}.Shared;");

        sb.AppendLine("");
        sb.AppendLine($"public interface I{tableName}Repository");
        sb.AppendLine("{");
        sb.AppendLine($"    Task<QueryResult<IEnumerable<{entityName}>>> ReadAsync(string? {ConvertStringToCamelCase(keyFieldName)});");
        sb.AppendLine($"    Task<QueryResult<IEnumerable<{entityName}>>> CreateAsync({entityName} {ConvertStringToCamelCase(entityName)});");
        sb.AppendLine($"    Task<QueryResult<IEnumerable<{entityName}>>> UpdateAsync({entityName} {ConvertStringToCamelCase(entityName)});");
        sb.AppendLine($"    Task<QueryResult<IEnumerable<{entityName}>>> DeleteAsync(string {ConvertStringToCamelCase(keyFieldName)});");
        sb.AppendLine("}");
        sb.AppendLine("");



        return sb.ToString();
    }


}