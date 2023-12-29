using CrudUi.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using CrudUi.Pages.Shared;
using System.Net.Http.Json;
using CrudUi.Pages.Features.Config.Models;

namespace CrudUi.Pages.Features.Config.Services;

public class TranslationsTypeService : ITranslationsTypeService
{
    private readonly HttpClient _http;
    private readonly Endpoints _endpoints;
    private readonly ILogger<TranslationsTypeService> _logger;

    public TranslationsTypeService(HttpClient http, IOptions<Endpoints> endpoints, ILogger<TranslationsTypeService> logger)
    {
        _http = http;
        _logger = logger;
        _endpoints = endpoints.Value;
    }

    public async Task<Result<List<TranslationsType>>> GetTranslationsTypesAsync()
    {
        var uri = new Uri(_endpoints.DataServiceBaseUri, $"translationstypes");
      
        try
        {
            var response = await _http.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var dataResult = await response.Content.ReadFromJsonAsync<QueryResult<IEnumerable<TranslationsType>>>();

                if (dataResult.Result && dataResult.Data != null)
                {
                    return new Result<List<TranslationsType>>(dataResult.Data.ToList());
                }
                else
                {
                    return new Result<List<TranslationsType>>(dataResult.Message);
                }
            }
            else
                return new Result<List<TranslationsType>>(response.ReasonPhrase);
        }
        catch (HttpRequestException reqEx)
        {
            return new Result<List<TranslationsType>>(reqEx);
        }
        catch (Exception ex)
        {
            return new Result<List<TranslationsType>>(ex);
        }
    }

    public async Task<Result<List<TranslationsType>>> CreateTranslationsTypeAsync(TranslationsType translationsType)
    {
        var uri = new Uri(_endpoints.DataServiceBaseUri, $"translationstypes");

        try
        {
            var response = await _http.PostAsync(uri, JsonContent.Create(translationsType));

            if (response.IsSuccessStatusCode)
            {
                var dataResult = await response.Content.ReadFromJsonAsync<QueryResult<IEnumerable<TranslationsType>>>();

                if (dataResult.Result && dataResult.Data != null)
                {
                    return new Result<List<TranslationsType>>(dataResult.Data.ToList());
                }
                else
                {
                    return new Result<List<TranslationsType>>(dataResult.Message);
                }
            }
            else
                return new Result<List<TranslationsType>>(response.ReasonPhrase);
        }
        catch (HttpRequestException reqEx)
        {
            return new Result<List<TranslationsType>>(reqEx);
        }
        catch (Exception ex)
        {
            return new Result<List<TranslationsType>>(ex);
        }
    }

    public async Task<Result<List<TranslationsType>>> UpdateTranslationsTypeAsync(TranslationsType translationsType)
    {
        var uri = new Uri(_endpoints.DataServiceBaseUri, $"translationstypes");

        try
        {
         

            var response = await _http.PutAsync(uri, JsonContent.Create(translationsType));

            if (response.IsSuccessStatusCode)
            {
                var dataResult = await response.Content.ReadFromJsonAsync<QueryResult<IEnumerable<TranslationsType>>>();

                if (dataResult.Result && dataResult.Data != null)
                {
                    return new Result<List<TranslationsType>>(dataResult.Data.ToList());
                }
                else
                {
                    return new Result<List<TranslationsType>>(dataResult.Message);
                }
            }
            else
                return new Result<List<TranslationsType>>(response.ReasonPhrase);
        }
        catch (HttpRequestException reqEx)
        {
            return new Result<List<TranslationsType>>(reqEx);
        }
        catch (Exception ex)
        {
            return new Result<List<TranslationsType>>(ex);
        }
    }

    public async Task<Result<List<TranslationsType>>> DeleteTranslationsTypeAsync(TranslationsType translationsType)
    {
        var uri = new Uri(_endpoints.DataServiceBaseUri, $"translationstypes/{translationsType.translation_type}");

        try
        {
            var response = await _http.DeleteAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var dataResult = await response.Content.ReadFromJsonAsync<QueryResult<IEnumerable<TranslationsType>>>();

                if (dataResult.Result && dataResult.Data != null)
                {
                    return new Result<List<TranslationsType>>(dataResult.Data.ToList());
                }
                else
                {
                    return new Result<List<TranslationsType>>(dataResult.Message);
                }
            }
            else
                return new Result<List<TranslationsType>>(response.ReasonPhrase);
        }
        catch (HttpRequestException reqEx)
        {
            return new Result<List<TranslationsType>>(reqEx);
        }
        catch (Exception ex)
        {
            return new Result<List<TranslationsType>>(ex);
        }
    }
}

