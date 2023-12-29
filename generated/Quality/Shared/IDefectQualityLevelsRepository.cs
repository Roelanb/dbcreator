using DataService.Config.Features.Quality.Entities;
using DataService.Shared.Entities;

namespace DataService.Config.Features.Quality.Shared;

public interface IDefectQualityLevelsRepository
{
    Task<QueryResult<IEnumerable<DefectQualityLevelModel>>> ReadAsync(string? defectQualityLevel);
    Task<QueryResult<IEnumerable<DefectQualityLevelModel>>> CreateAsync(DefectQualityLevelModel defectQualityLevelModel);
    Task<QueryResult<IEnumerable<DefectQualityLevelModel>>> UpdateAsync(DefectQualityLevelModel defectQualityLevelModel);
    Task<QueryResult<IEnumerable<DefectQualityLevelModel>>> DeleteAsync(string defectQualityLevel);
}

