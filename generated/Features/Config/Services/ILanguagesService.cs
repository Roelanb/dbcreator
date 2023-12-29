using CrudUi.Pages.Features.Config.Models;
using CrudUi.Pages.Shared;

namespace CrudUi.Pages.Features.Config.Services;

public interface ILanguageService
{
    Task<Result<List<Language>>> GetLanguagesAsync();
    Task<Result<List<Language>>> CreateLanguageAsync(Language language);
    Task<Result<List<Language>>> UpdateLanguageAsync(Language language);
    Task<Result<List<Language>>> DeleteLanguageAsync(Language language);
}

