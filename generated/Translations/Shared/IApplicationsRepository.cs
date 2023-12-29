using DataService.Config.Features.Translations.Entities;
using DataService.Shared.Entities;

namespace DataService.Config.Features.Translations.Shared;

public interface IApplicationsRepository
{
    Task<QueryResult<IEnumerable<Application>>> ReadAsync(string? name);
    Task<QueryResult<IEnumerable<Application>>> CreateAsync(Application application);
    Task<QueryResult<IEnumerable<Application>>> UpdateAsync(Application application);
    Task<QueryResult<IEnumerable<Application>>> DeleteAsync(string name);
}

