using CrudUi.Pages.Features.Config.Models;
using CrudUi.Pages.Shared;

namespace CrudUi.Pages.Features.Config.Services;

public interface IApplicationService
{
    Task<Result<List<Application>>> GetApplicationsAsync();
    Task<Result<List<Application>>> CreateApplicationAsync(Application application);
    Task<Result<List<Application>>> UpdateApplicationAsync(Application application);
    Task<Result<List<Application>>> DeleteApplicationAsync(Application application);
}

