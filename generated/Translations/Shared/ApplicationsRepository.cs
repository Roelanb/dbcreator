using System.Data;
using Dapper;
using DataService.Config.Features.Translations.Entities;
using DataService.Shared.Configuration;
using DataService.Shared.Entities;
using Microsoft.Data.SqlClient;

namespace DataService.Config.Features.Translations.Shared;

public class ApplicationsRepository : IApplicationsRepository
{
    private readonly ConnectionStringProvider _connectionStringProvider;

    public ApplicationsRepository(ConnectionStringProvider connectionStringProvider)
    {
        _connectionStringProvider = connectionStringProvider;
    }

    public async Task<QueryResult<IEnumerable<Application>>> ReadAsync(string? name)
    {
        try
        {
            var result = new QueryResult<IEnumerable<Application>>();

            var connectionString = _connectionStringProvider.GetConnectionStrings(ConnectionStringKeys.MES).First();
            using IDbConnection connection = new SqlConnection(connectionString.Value);
            connection.Open();
            var parameters = new DynamicParameters();
             if (name != null) parameters.Add("Name", name, DbType.String);


            result.Data = await connection.QueryAsync<Application>("csp_dlm_applications_read", parameters, commandType: CommandType.StoredProcedure);


            return result;
        }
        catch (Exception ex)
        {

            return new QueryResult<IEnumerable<Application>>(ex.Message);
        }
    }

public async Task<QueryResult<IEnumerable<Application>>> CreateAsync(Application application)

    {
        try
        {
            var result = new QueryResult<IEnumerable<Application>>();

            var connectionString = _connectionStringProvider.GetConnectionStrings(ConnectionStringKeys.MES).First();
            using IDbConnection connection = new SqlConnection(connectionString.Value);
            connection.Open();
            //Set up DynamicParameters object to pass parameters  
            var parameters = new DynamicParameters();
            parameters.Add("Name", application.Name, DbType.String);
            parameters.Add("Description", application.Description, DbType.String);
            parameters.Add("IsActive", application.IsActive, DbType.Boolean);

            //Execute stored procedure and map the returned result to a Customer object  
            result.Data = await connection.QueryAsync<Application>("csp_dlm_applications_create", parameters, commandType: CommandType.StoredProcedure);

            return result;
        }
        catch (Exception ex)
        {

            return new QueryResult<IEnumerable<Application>>(ex.Message);
        }
    }

public async Task<QueryResult<IEnumerable<Application>>> UpdateAsync(Application application)

    {
        try
        {
            var result = new QueryResult<IEnumerable<Application>>();

            var connectionString = _connectionStringProvider.GetConnectionStrings(ConnectionStringKeys.MES).First();
            using IDbConnection connection = new SqlConnection(connectionString.Value);
            connection.Open();
            //Set up DynamicParameters object to pass parameters  
            var parameters = new DynamicParameters();
            parameters.Add("Name", application.Name, DbType.String);
            parameters.Add("Description", application.Description, DbType.String);
            parameters.Add("IsActive", application.IsActive, DbType.Boolean);

            //Execute stored procedure and map the returned result to a Customer object  
            result.Data = await connection.QueryAsync<Application>("csp_dlm_applications_update", parameters, commandType: CommandType.StoredProcedure);

            return result;
        }
        catch (Exception ex)
        {

            return new QueryResult<IEnumerable<Application>>(ex.Message);
        }
    }

public async Task<QueryResult<IEnumerable<Application>>> DeleteAsync(string name)

    {
        try
        {
            var result = new QueryResult<IEnumerable<Application>>();

            var connectionString = _connectionStringProvider.GetConnectionStrings(ConnectionStringKeys.MES).First();
            using IDbConnection connection = new SqlConnection(connectionString.Value);
            connection.Open();
            //Set up DynamicParameters object to pass parameters  
            var parameters = new DynamicParameters();
            parameters.Add("Name", name, DbType.String);

            //Execute stored procedure and map the returned result to a Customer object  
            result.Data = await connection.QueryAsync<Application>("csp_dlm_applications_delete", parameters, commandType: CommandType.StoredProcedure);

            return result;
        }
        catch (Exception ex)
        {

            return new QueryResult<IEnumerable<Application>>(ex.Message);
        }
    }
}

