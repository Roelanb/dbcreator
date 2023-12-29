using CrudUi.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using CrudUi.Pages.Shared;
using System.Net.Http.Json;
using CrudUi.Pages.Features.Config.Models;

namespace CrudUi.Pages.Features.Config.Services;

public class LanguageService : ILanguageService
{
    private readonly HttpClient _http;
    private readonly Endpoints _endpoints;
    private readonly ILogger<LanguageService> _logger;

    public LanguageService(HttpClient http, IOptions<Endpoints> endpoints, ILogger<LanguageService> logger)
    {
        _http = http;
        _logger = logger;
        _endpoints = endpoints.Value;
    }

    public async Task<Result<List<Language>>> GetLanguagesAsync()
    {
        var uri = new Uri(_endpoints.DataServiceBaseUri, $"languages");
      
        try
        {
            var response = await _http.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var dataResult = await response.Content.ReadFromJsonAsync<QueryResult<IEnumerable<Language>>>();

                if (dataResult.Result && dataResult.Data != null)
                {
                    return new Result<List<Language>>(dataResult.Data.ToList());
                }
                else
                {
                    return new Result<List<Language>>(dataResult.Message);
                }
            }
            else
                return new Result<List<Language>>(response.ReasonPhrase);
        }
        catch (HttpRequestException reqEx)
        {
            return new Result<List<Language>>(reqEx);
        }
        catch (Exception ex)
        {
            return new Result<List<Language>>(ex);
        }
    }

    public async Task<Result<List<Language>>> CreateLanguageAsync(Language language)
    {
        var uri = new Uri(_endpoints.DataServiceBaseUri, $"languages");

        try
        {
            var response = await _http.PostAsync(uri, JsonContent.Create(language));

            if (response.IsSuccessStatusCode)
            {
                var dataResult = await response.Content.ReadFromJsonAsync<QueryResult<IEnumerable<Language>>>();

                if (dataResult.Result && dataResult.Data != null)
                {
                    return new Result<List<Language>>(dataResult.Data.ToList());
                }
                else
                {
                    return new Result<List<Language>>(dataResult.Message);
                }
            }
            else
                return new Result<List<Language>>(response.ReasonPhrase);
        }
        catch (HttpRequestException reqEx)
        {
            return new Result<List<Language>>(reqEx);
        }
        catch (Exception ex)
        {
            return new Result<List<Language>>(ex);
        }
    }

    public async Task<Result<List<Language>>> UpdateLanguageAsync(Language language)
    {
        var uri = new Uri(_endpoints.DataServiceBaseUri, $"languages");

        try
        {
         

            var response = await _http.PutAsync(uri, JsonContent.Create(language));

            if (response.IsSuccessStatusCode)
            {
                var dataResult = await response.Content.ReadFromJsonAsync<QueryResult<IEnumerable<Language>>>();

                if (dataResult.Result && dataResult.Data != null)
                {
                    return new Result<List<Language>>(dataResult.Data.ToList());
                }
                else
                {
                    return new Result<List<Language>>(dataResult.Message);
                }
            }
            else
                return new Result<List<Language>>(response.ReasonPhrase);
        }
        catch (HttpRequestException reqEx)
        {
            return new Result<List<Language>>(reqEx);
        }
        catch (Exception ex)
        {
            return new Result<List<Language>>(ex);
        }
    }

    public async Task<Result<List<Language>>> DeleteLanguageAsync(Language language)
    {
        var uri = new Uri(_endpoints.DataServiceBaseUri, $"languages/{language.Name}");

        try
        {
            var response = await _http.DeleteAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var dataResult = await response.Content.ReadFromJsonAsync<QueryResult<IEnumerable<Language>>>();

                if (dataResult.Result && dataResult.Data != null)
                {
                    return new Result<List<Language>>(dataResult.Data.ToList());
                }
                else
                {
                    return new Result<List<Language>>(dataResult.Message);
                }
            }
            else
                return new Result<List<Language>>(response.ReasonPhrase);
        }
        catch (HttpRequestException reqEx)
        {
            return new Result<List<Language>>(reqEx);
        }
        catch (Exception ex)
        {
            return new Result<List<Language>>(ex);
        }
    }
}

