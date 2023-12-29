using DataService.Config.Features.Quality.Entities;
using DataService.Shared.Entities;

namespace DataService.Config.Features.Quality.Shared;

public interface IDefectPieceRefsRepository
{
    Task<QueryResult<IEnumerable<DefectPieceRefModel>>> ReadAsync(string? pieceRef);
    Task<QueryResult<IEnumerable<DefectPieceRefModel>>> CreateAsync(DefectPieceRefModel defectPieceRefModel);
    Task<QueryResult<IEnumerable<DefectPieceRefModel>>> UpdateAsync(DefectPieceRefModel defectPieceRefModel);
    Task<QueryResult<IEnumerable<DefectPieceRefModel>>> DeleteAsync(string pieceRef);
}

