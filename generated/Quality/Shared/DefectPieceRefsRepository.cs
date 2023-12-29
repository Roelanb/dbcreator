using System.Data;
using Dapper;
using DataService.Config.Features.Quality.Entities;
using DataService.Shared.Configuration;
using DataService.Shared.Entities;
using Microsoft.Data.SqlClient;

namespace DataService.Config.Features.Quality.Shared;

public class DefectPieceRefsRepository : IDefectPieceRefsRepository
{
    private readonly ConnectionStringProvider _connectionStringProvider;

    public DefectPieceRefsRepository(ConnectionStringProvider connectionStringProvider)
    {
        _connectionStringProvider = connectionStringProvider;
    }

    public async Task<QueryResult<IEnumerable<DefectPieceRefModel>>> ReadAsync(string? pieceRef)
    {
        try
        {
            var result = new QueryResult<IEnumerable<DefectPieceRefModel>>();

            var connectionString = _connectionStringProvider.GetConnectionStrings(ConnectionStringKeys.MES).First();
            using IDbConnection connection = new SqlConnection(connectionString.Value);
            connection.Open();
            var parameters = new DynamicParameters();
             if (pieceRef != null) parameters.Add("PieceRef", pieceRef, DbType.String);


            result.Data = await connection.QueryAsync<DefectPieceRefModel>("csp_dlm_defectpiecerefs_read", parameters, commandType: CommandType.StoredProcedure);


            return result;
        }
        catch (Exception ex)
        {

            return new QueryResult<IEnumerable<DefectPieceRefModel>>(ex.Message);
        }
    }

public async Task<QueryResult<IEnumerable<DefectPieceRefModel>>> CreateAsync(DefectPieceRefModel defectPieceRefModel)

    {
        try
        {
            var result = new QueryResult<IEnumerable<DefectPieceRefModel>>();

            var connectionString = _connectionStringProvider.GetConnectionStrings(ConnectionStringKeys.MES).First();
            using IDbConnection connection = new SqlConnection(connectionString.Value);
            connection.Open();
            //Set up DynamicParameters object to pass parameters  
            var parameters = new DynamicParameters();
            parameters.Add("PieceRef", defectPieceRefModel.PieceRef, DbType.String);
            parameters.Add("OnBody", defectPieceRefModel.OnBody, DbType.Boolean);
            parameters.Add("OnCap", defectPieceRefModel.OnCap, DbType.Boolean);
            parameters.Add("English_Desc", defectPieceRefModel.English_Desc, DbType.String);
            parameters.Add("ReadOnly", defectPieceRefModel.ReadOnly, DbType.Boolean);
            parameters.Add("Defect_Desc", defectPieceRefModel.Defect_Desc, DbType.String);
            parameters.Add("IsActive", defectPieceRefModel.IsActive, DbType.Boolean);

            //Execute stored procedure and map the returned result to a Customer object  
            result.Data = await connection.QueryAsync<DefectPieceRefModel>("csp_dlm_defectpiecerefs_create", parameters, commandType: CommandType.StoredProcedure);

            return result;
        }
        catch (Exception ex)
        {

            return new QueryResult<IEnumerable<DefectPieceRefModel>>(ex.Message);
        }
    }

public async Task<QueryResult<IEnumerable<DefectPieceRefModel>>> UpdateAsync(DefectPieceRefModel defectPieceRefModel)

    {
        try
        {
            var result = new QueryResult<IEnumerable<DefectPieceRefModel>>();

            var connectionString = _connectionStringProvider.GetConnectionStrings(ConnectionStringKeys.MES).First();
            using IDbConnection connection = new SqlConnection(connectionString.Value);
            connection.Open();
            //Set up DynamicParameters object to pass parameters  
            var parameters = new DynamicParameters();
            parameters.Add("PieceRef", defectPieceRefModel.PieceRef, DbType.String);
            parameters.Add("OnBody", defectPieceRefModel.OnBody, DbType.Boolean);
            parameters.Add("OnCap", defectPieceRefModel.OnCap, DbType.Boolean);
            parameters.Add("English_Desc", defectPieceRefModel.English_Desc, DbType.String);
            parameters.Add("ReadOnly", defectPieceRefModel.ReadOnly, DbType.Boolean);
            parameters.Add("Defect_Desc", defectPieceRefModel.Defect_Desc, DbType.String);
            parameters.Add("IsActive", defectPieceRefModel.IsActive, DbType.Boolean);

            //Execute stored procedure and map the returned result to a Customer object  
            result.Data = await connection.QueryAsync<DefectPieceRefModel>("csp_dlm_defectpiecerefs_update", parameters, commandType: CommandType.StoredProcedure);

            return result;
        }
        catch (Exception ex)
        {

            return new QueryResult<IEnumerable<DefectPieceRefModel>>(ex.Message);
        }
    }

public async Task<QueryResult<IEnumerable<DefectPieceRefModel>>> DeleteAsync(string pieceRef)

    {
        try
        {
            var result = new QueryResult<IEnumerable<DefectPieceRefModel>>();

            var connectionString = _connectionStringProvider.GetConnectionStrings(ConnectionStringKeys.MES).First();
            using IDbConnection connection = new SqlConnection(connectionString.Value);
            connection.Open();
            //Set up DynamicParameters object to pass parameters  
            var parameters = new DynamicParameters();
            parameters.Add("PieceRef", pieceRef, DbType.String);

            //Execute stored procedure and map the returned result to a Customer object  
            result.Data = await connection.QueryAsync<DefectPieceRefModel>("csp_dlm_defectpiecerefs_delete", parameters, commandType: CommandType.StoredProcedure);

            return result;
        }
        catch (Exception ex)
        {

            return new QueryResult<IEnumerable<DefectPieceRefModel>>(ex.Message);
        }
    }
}

