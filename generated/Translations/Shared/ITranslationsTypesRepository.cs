using DataService.Config.Features.Translations.Entities;
using DataService.Shared.Entities;

namespace DataService.Config.Features.Translations.Shared;

public interface ITranslationsTypesRepository
{
    Task<QueryResult<IEnumerable<TranslationsType>>> ReadAsync(string? translation_type);
    Task<QueryResult<IEnumerable<TranslationsType>>> CreateAsync(TranslationsType translationsType);
    Task<QueryResult<IEnumerable<TranslationsType>>> UpdateAsync(TranslationsType translationsType);
    Task<QueryResult<IEnumerable<TranslationsType>>> DeleteAsync(string translation_type);
}

