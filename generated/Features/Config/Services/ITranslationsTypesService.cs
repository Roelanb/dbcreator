using CrudUi.Pages.Features.Config.Models;
using CrudUi.Pages.Shared;

namespace CrudUi.Pages.Features.Config.Services;

public interface ITranslationsTypeService
{
    Task<Result<List<TranslationsType>>> GetTranslationsTypesAsync();
    Task<Result<List<TranslationsType>>> CreateTranslationsTypeAsync(TranslationsType translationsType);
    Task<Result<List<TranslationsType>>> UpdateTranslationsTypeAsync(TranslationsType translationsType);
    Task<Result<List<TranslationsType>>> DeleteTranslationsTypeAsync(TranslationsType translationsType);
}

