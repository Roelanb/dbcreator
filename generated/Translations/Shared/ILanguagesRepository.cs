using DataService.Config.Features.Translations.Entities;
using DataService.Shared.Entities;

namespace DataService.Config.Features.Translations.Shared;

public interface ILanguagesRepository
{
    Task<QueryResult<IEnumerable<Language>>> ReadAsync(string? name);
    Task<QueryResult<IEnumerable<Language>>> CreateAsync(Language language);
    Task<QueryResult<IEnumerable<Language>>> UpdateAsync(Language language);
    Task<QueryResult<IEnumerable<Language>>> DeleteAsync(string name);
}

