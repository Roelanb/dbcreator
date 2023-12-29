using System.Data;
using Dapper;
using DataService.Config.Features.Translations.Entities;
using DataService.Shared.Configuration;
using DataService.Shared.Entities;
using Microsoft.Data.SqlClient;

namespace DataService.Config.Features.Translations.Shared;

public class TranslationsTypesRepository : ITranslationsTypesRepository
{
    private readonly ConnectionStringProvider _connectionStringProvider;

    public TranslationsTypesRepository(ConnectionStringProvider connectionStringProvider)
    {
        _connectionStringProvider = connectionStringProvider;
    }

    public async Task<QueryResult<IEnumerable<TranslationsType>>> ReadAsync(string? translation_type)
    {
        try
        {
            var result = new QueryResult<IEnumerable<TranslationsType>>();

            var connectionString = _connectionStringProvider.GetConnectionStrings(ConnectionStringKeys.MES).First();
            using IDbConnection connection = new SqlConnection(connectionString.Value);
            connection.Open();
            var parameters = new DynamicParameters();
             if (translation_type != null) parameters.Add("translation_type", translation_type, DbType.String);


            result.Data = await connection.QueryAsync<TranslationsType>("csp_dlm_translationstypes_read", parameters, commandType: CommandType.StoredProcedure);


            return result;
        }
        catch (Exception ex)
        {

            return new QueryResult<IEnumerable<TranslationsType>>(ex.Message);
        }
    }

public async Task<QueryResult<IEnumerable<TranslationsType>>> CreateAsync(TranslationsType translationsType)

    {
        try
        {
            var result = new QueryResult<IEnumerable<TranslationsType>>();

            var connectionString = _connectionStringProvider.GetConnectionStrings(ConnectionStringKeys.MES).First();
            using IDbConnection connection = new SqlConnection(connectionString.Value);
            connection.Open();
            //Set up DynamicParameters object to pass parameters  
            var parameters = new DynamicParameters();
            parameters.Add("translation_type", translationsType.translation_type, DbType.String);
            parameters.Add("IsActive", translationsType.IsActive, DbType.Boolean);
            parameters.Add("Description", translationsType.Description, DbType.String);

            //Execute stored procedure and map the returned result to a Customer object  
            result.Data = await connection.QueryAsync<TranslationsType>("csp_dlm_translationstypes_create", parameters, commandType: CommandType.StoredProcedure);

            return result;
        }
        catch (Exception ex)
        {

            return new QueryResult<IEnumerable<TranslationsType>>(ex.Message);
        }
    }

public async Task<QueryResult<IEnumerable<TranslationsType>>> UpdateAsync(TranslationsType translationsType)

    {
        try
        {
            var result = new QueryResult<IEnumerable<TranslationsType>>();

            var connectionString = _connectionStringProvider.GetConnectionStrings(ConnectionStringKeys.MES).First();
            using IDbConnection connection = new SqlConnection(connectionString.Value);
            connection.Open();
            //Set up DynamicParameters object to pass parameters  
            var parameters = new DynamicParameters();
            parameters.Add("translation_type", translationsType.translation_type, DbType.String);
            parameters.Add("IsActive", translationsType.IsActive, DbType.Boolean);
            parameters.Add("Description", translationsType.Description, DbType.String);

            //Execute stored procedure and map the returned result to a Customer object  
            result.Data = await connection.QueryAsync<TranslationsType>("csp_dlm_translationstypes_update", parameters, commandType: CommandType.StoredProcedure);

            return result;
        }
        catch (Exception ex)
        {

            return new QueryResult<IEnumerable<TranslationsType>>(ex.Message);
        }
    }

public async Task<QueryResult<IEnumerable<TranslationsType>>> DeleteAsync(string translation_type)

    {
        try
        {
            var result = new QueryResult<IEnumerable<TranslationsType>>();

            var connectionString = _connectionStringProvider.GetConnectionStrings(ConnectionStringKeys.MES).First();
            using IDbConnection connection = new SqlConnection(connectionString.Value);
            connection.Open();
            //Set up DynamicParameters object to pass parameters  
            var parameters = new DynamicParameters();
            parameters.Add("translation_type", translation_type, DbType.String);

            //Execute stored procedure and map the returned result to a Customer object  
            result.Data = await connection.QueryAsync<TranslationsType>("csp_dlm_translationstypes_delete", parameters, commandType: CommandType.StoredProcedure);

            return result;
        }
        catch (Exception ex)
        {

            return new QueryResult<IEnumerable<TranslationsType>>(ex.Message);
        }
    }
}

