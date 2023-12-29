using System.Data;
using Dapper;
using DataService.Config.Features.Quality.Entities;
using DataService.Shared.Configuration;
using DataService.Shared.Entities;
using Microsoft.Data.SqlClient;

namespace DataService.Config.Features.Quality.Shared;

public class DefectQualityLevelsRepository : IDefectQualityLevelsRepository
{
    private readonly ConnectionStringProvider _connectionStringProvider;

    public DefectQualityLevelsRepository(ConnectionStringProvider connectionStringProvider)
    {
        _connectionStringProvider = connectionStringProvider;
    }

    public async Task<QueryResult<IEnumerable<DefectQualityLevelModel>>> ReadAsync(string? defectQualityLevel)
    {
        try
        {
            var result = new QueryResult<IEnumerable<DefectQualityLevelModel>>();

            var connectionString = _connectionStringProvider.GetConnectionStrings(ConnectionStringKeys.MES).First();
            using IDbConnection connection = new SqlConnection(connectionString.Value);
            connection.Open();
            var parameters = new DynamicParameters();
             if (defectQualityLevel != null) parameters.Add("DefectQualityLevel", defectQualityLevel, DbType.String);


            result.Data = await connection.QueryAsync<DefectQualityLevelModel>("csp_dlm_defectqualitylevels_read", parameters, commandType: CommandType.StoredProcedure);


            return result;
        }
        catch (Exception ex)
        {

            return new QueryResult<IEnumerable<DefectQualityLevelModel>>(ex.Message);
        }
    }

public async Task<QueryResult<IEnumerable<DefectQualityLevelModel>>> CreateAsync(DefectQualityLevelModel defectQualityLevelModel)

    {
        try
        {
            var result = new QueryResult<IEnumerable<DefectQualityLevelModel>>();

            var connectionString = _connectionStringProvider.GetConnectionStrings(ConnectionStringKeys.MES).First();
            using IDbConnection connection = new SqlConnection(connectionString.Value);
            connection.Open();
            //Set up DynamicParameters object to pass parameters  
            var parameters = new DynamicParameters();
            parameters.Add("DefectQualityLevel", defectQualityLevelModel.DefectQualityLevel, DbType.String);
            parameters.Add("English_Desc", defectQualityLevelModel.English_Desc, DbType.String);
            parameters.Add("QALevelRanking", defectQualityLevelModel.QALevelRanking, DbType.Int16);
            parameters.Add("ReadOnly", defectQualityLevelModel.ReadOnly, DbType.Boolean);
            parameters.Add("IsActive", defectQualityLevelModel.IsActive, DbType.Boolean);

            //Execute stored procedure and map the returned result to a Customer object  
            result.Data = await connection.QueryAsync<DefectQualityLevelModel>("csp_dlm_defectqualitylevels_create", parameters, commandType: CommandType.StoredProcedure);

            return result;
        }
        catch (Exception ex)
        {

            return new QueryResult<IEnumerable<DefectQualityLevelModel>>(ex.Message);
        }
    }

public async Task<QueryResult<IEnumerable<DefectQualityLevelModel>>> UpdateAsync(DefectQualityLevelModel defectQualityLevelModel)

    {
        try
        {
            var result = new QueryResult<IEnumerable<DefectQualityLevelModel>>();

            var connectionString = _connectionStringProvider.GetConnectionStrings(ConnectionStringKeys.MES).First();
            using IDbConnection connection = new SqlConnection(connectionString.Value);
            connection.Open();
            //Set up DynamicParameters object to pass parameters  
            var parameters = new DynamicParameters();
            parameters.Add("DefectQualityLevel", defectQualityLevelModel.DefectQualityLevel, DbType.String);
            parameters.Add("English_Desc", defectQualityLevelModel.English_Desc, DbType.String);
            parameters.Add("QALevelRanking", defectQualityLevelModel.QALevelRanking, DbType.Int16);
            parameters.Add("ReadOnly", defectQualityLevelModel.ReadOnly, DbType.Boolean);
            parameters.Add("IsActive", defectQualityLevelModel.IsActive, DbType.Boolean);

            //Execute stored procedure and map the returned result to a Customer object  
            result.Data = await connection.QueryAsync<DefectQualityLevelModel>("csp_dlm_defectqualitylevels_update", parameters, commandType: CommandType.StoredProcedure);

            return result;
        }
        catch (Exception ex)
        {

            return new QueryResult<IEnumerable<DefectQualityLevelModel>>(ex.Message);
        }
    }

public async Task<QueryResult<IEnumerable<DefectQualityLevelModel>>> DeleteAsync(string defectQualityLevel)

    {
        try
        {
            var result = new QueryResult<IEnumerable<DefectQualityLevelModel>>();

            var connectionString = _connectionStringProvider.GetConnectionStrings(ConnectionStringKeys.MES).First();
            using IDbConnection connection = new SqlConnection(connectionString.Value);
            connection.Open();
            //Set up DynamicParameters object to pass parameters  
            var parameters = new DynamicParameters();
            parameters.Add("DefectQualityLevel", defectQualityLevel, DbType.String);

            //Execute stored procedure and map the returned result to a Customer object  
            result.Data = await connection.QueryAsync<DefectQualityLevelModel>("csp_dlm_defectqualitylevels_delete", parameters, commandType: CommandType.StoredProcedure);

            return result;
        }
        catch (Exception ex)
        {

            return new QueryResult<IEnumerable<DefectQualityLevelModel>>(ex.Message);
        }
    }
}

