using System.Data;
using Dapper;
using DataService.Config.Features.Translations.Entities;
using DataService.Shared.Configuration;
using DataService.Shared.Entities;
using Microsoft.Data.SqlClient;

namespace DataService.Config.Features.Translations.Shared;

public class LanguagesRepository : ILanguagesRepository
{
    private readonly ConnectionStringProvider _connectionStringProvider;

    public LanguagesRepository(ConnectionStringProvider connectionStringProvider)
    {
        _connectionStringProvider = connectionStringProvider;
    }

    public async Task<QueryResult<IEnumerable<Language>>> ReadAsync(string? name)
    {
        try
        {
            var result = new QueryResult<IEnumerable<Language>>();

            var connectionString = _connectionStringProvider.GetConnectionStrings(ConnectionStringKeys.MES).First();
            using IDbConnection connection = new SqlConnection(connectionString.Value);
            connection.Open();
            var parameters = new DynamicParameters();
             if (name != null) parameters.Add("Name", name, DbType.String);


            result.Data = await connection.QueryAsync<Language>("csp_dlm_languages_read", parameters, commandType: CommandType.StoredProcedure);


            return result;
        }
        catch (Exception ex)
        {

            return new QueryResult<IEnumerable<Language>>(ex.Message);
        }
    }

public async Task<QueryResult<IEnumerable<Language>>> CreateAsync(Language language)

    {
        try
        {
            var result = new QueryResult<IEnumerable<Language>>();

            var connectionString = _connectionStringProvider.GetConnectionStrings(ConnectionStringKeys.MES).First();
            using IDbConnection connection = new SqlConnection(connectionString.Value);
            connection.Open();
            //Set up DynamicParameters object to pass parameters  
            var parameters = new DynamicParameters();
            parameters.Add("Name", language.Name, DbType.String);
            parameters.Add("IsActive", language.IsActive, DbType.Boolean);

            //Execute stored procedure and map the returned result to a Customer object  
            result.Data = await connection.QueryAsync<Language>("csp_dlm_languages_create", parameters, commandType: CommandType.StoredProcedure);

            return result;
        }
        catch (Exception ex)
        {

            return new QueryResult<IEnumerable<Language>>(ex.Message);
        }
    }

public async Task<QueryResult<IEnumerable<Language>>> UpdateAsync(Language language)

    {
        try
        {
            var result = new QueryResult<IEnumerable<Language>>();

            var connectionString = _connectionStringProvider.GetConnectionStrings(ConnectionStringKeys.MES).First();
            using IDbConnection connection = new SqlConnection(connectionString.Value);
            connection.Open();
            //Set up DynamicParameters object to pass parameters  
            var parameters = new DynamicParameters();
            parameters.Add("Name", language.Name, DbType.String);
            parameters.Add("IsActive", language.IsActive, DbType.Boolean);

            //Execute stored procedure and map the returned result to a Customer object  
            result.Data = await connection.QueryAsync<Language>("csp_dlm_languages_update", parameters, commandType: CommandType.StoredProcedure);

            return result;
        }
        catch (Exception ex)
        {

            return new QueryResult<IEnumerable<Language>>(ex.Message);
        }
    }

public async Task<QueryResult<IEnumerable<Language>>> DeleteAsync(string name)

    {
        try
        {
            var result = new QueryResult<IEnumerable<Language>>();

            var connectionString = _connectionStringProvider.GetConnectionStrings(ConnectionStringKeys.MES).First();
            using IDbConnection connection = new SqlConnection(connectionString.Value);
            connection.Open();
            //Set up DynamicParameters object to pass parameters  
            var parameters = new DynamicParameters();
            parameters.Add("Name", name, DbType.String);

            //Execute stored procedure and map the returned result to a Customer object  
            result.Data = await connection.QueryAsync<Language>("csp_dlm_languages_delete", parameters, commandType: CommandType.StoredProcedure);

            return result;
        }
        catch (Exception ex)
        {

            return new QueryResult<IEnumerable<Language>>(ex.Message);
        }
    }
}

