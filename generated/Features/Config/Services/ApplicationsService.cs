using CrudUi.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using CrudUi.Pages.Shared;
using System.Net.Http.Json;
using CrudUi.Pages.Features.Config.Models;

namespace CrudUi.Pages.Features.Config.Services;

public class ApplicationService : IApplicationService
{
    private readonly HttpClient _http;
    private readonly Endpoints _endpoints;
    private readonly ILogger<ApplicationService> _logger;

    public ApplicationService(HttpClient http, IOptions<Endpoints> endpoints, ILogger<ApplicationService> logger)
    {
        _http = http;
        _logger = logger;
        _endpoints = endpoints.Value;
    }

    public async Task<Result<List<Application>>> GetApplicationsAsync()
    {
        var uri = new Uri(_endpoints.DataServiceBaseUri, $"applications");
      
        try
        {
            var response = await _http.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var dataResult = await response.Content.ReadFromJsonAsync<QueryResult<IEnumerable<Application>>>();

                if (dataResult.Result && dataResult.Data != null)
                {
                    return new Result<List<Application>>(dataResult.Data.ToList());
                }
                else
                {
                    return new Result<List<Application>>(dataResult.Message);
                }
            }
            else
                return new Result<List<Application>>(response.ReasonPhrase);
        }
        catch (HttpRequestException reqEx)
        {
            return new Result<List<Application>>(reqEx);
        }
        catch (Exception ex)
        {
            return new Result<List<Application>>(ex);
        }
    }

    public async Task<Result<List<Application>>> CreateApplicationAsync(Application application)
    {
        var uri = new Uri(_endpoints.DataServiceBaseUri, $"applications");

        try
        {
            var response = await _http.PostAsync(uri, JsonContent.Create(application));

            if (response.IsSuccessStatusCode)
            {
                var dataResult = await response.Content.ReadFromJsonAsync<QueryResult<IEnumerable<Application>>>();

                if (dataResult.Result && dataResult.Data != null)
                {
                    return new Result<List<Application>>(dataResult.Data.ToList());
                }
                else
                {
                    return new Result<List<Application>>(dataResult.Message);
                }
            }
            else
                return new Result<List<Application>>(response.ReasonPhrase);
        }
        catch (HttpRequestException reqEx)
        {
            return new Result<List<Application>>(reqEx);
        }
        catch (Exception ex)
        {
            return new Result<List<Application>>(ex);
        }
    }

    public async Task<Result<List<Application>>> UpdateApplicationAsync(Application application)
    {
        var uri = new Uri(_endpoints.DataServiceBaseUri, $"applications");

        try
        {
         

            var response = await _http.PutAsync(uri, JsonContent.Create(application));

            if (response.IsSuccessStatusCode)
            {
                var dataResult = await response.Content.ReadFromJsonAsync<QueryResult<IEnumerable<Application>>>();

                if (dataResult.Result && dataResult.Data != null)
                {
                    return new Result<List<Application>>(dataResult.Data.ToList());
                }
                else
                {
                    return new Result<List<Application>>(dataResult.Message);
                }
            }
            else
                return new Result<List<Application>>(response.ReasonPhrase);
        }
        catch (HttpRequestException reqEx)
        {
            return new Result<List<Application>>(reqEx);
        }
        catch (Exception ex)
        {
            return new Result<List<Application>>(ex);
        }
    }

    public async Task<Result<List<Application>>> DeleteApplicationAsync(Application application)
    {
        var uri = new Uri(_endpoints.DataServiceBaseUri, $"applications/{application.Name}");

        try
        {
            var response = await _http.DeleteAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var dataResult = await response.Content.ReadFromJsonAsync<QueryResult<IEnumerable<Application>>>();

                if (dataResult.Result && dataResult.Data != null)
                {
                    return new Result<List<Application>>(dataResult.Data.ToList());
                }
                else
                {
                    return new Result<List<Application>>(dataResult.Message);
                }
            }
            else
                return new Result<List<Application>>(response.ReasonPhrase);
        }
        catch (HttpRequestException reqEx)
        {
            return new Result<List<Application>>(reqEx);
        }
        catch (Exception ex)
        {
            return new Result<List<Application>>(ex);
        }
    }
}

