using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;

public partial class Converter
{

    public string GenerateServicesClassString(ObjectToMap o)
    {
        Console.WriteLine($"Generating service class for {o.TableName}");

        var sb = new StringBuilder();
    
        sb.AppendLine($"using {o.UiNameSpace.Replace(".Pages","")}.Configuration;");
        sb.AppendLine("using Microsoft.Extensions.Options;");
        sb.AppendLine("using Microsoft.Extensions.Logging;");
        sb.AppendLine($"using {o.UiNameSpace}.Shared;");
        sb.AppendLine("using System.Net.Http.Json;");
        sb.AppendLine($"using {o.UiNameSpace}.Features.Config.Models;");

        sb.AppendLine("");
        sb.AppendLine($"namespace {o.UiNameSpace}.Features.Config.Services;");

        sb.AppendLine("");
        sb.AppendLine($"public class {o.EntityName}Service : I{o.EntityName}Service");
        sb.AppendLine("{");
        sb.AppendLine("    private readonly HttpClient _http;");
        sb.AppendLine("    private readonly Endpoints _endpoints;");
        sb.AppendLine($"    private readonly ILogger<{o.EntityName}Service> _logger;");
        sb.AppendLine("");
        sb.AppendLine($"    public {o.EntityName}Service(HttpClient http, IOptions<Endpoints> endpoints, ILogger<{o.EntityName}Service> logger)");
        sb.AppendLine("    {");
        sb.AppendLine("        _http = http;");
        sb.AppendLine("        _logger = logger;");
        sb.AppendLine("        _endpoints = endpoints.Value;");
        sb.AppendLine("    }");
        sb.AppendLine("");
        sb.AppendLine($"    public async Task<Result<List<{o.EntityName}>>> Get{o.TableName}Async()");
        sb.AppendLine("    {");
        sb.AppendLine($"        var uri = new Uri(_endpoints.DataServiceBaseUri, $\"{o.TableName.ToLower()}\");");
        sb.AppendLine("      ");
        sb.AppendLine("        try");
        sb.AppendLine("        {");
        sb.AppendLine("            var response = await _http.GetAsync(uri);");
        sb.AppendLine("");
        sb.AppendLine("            if (response.IsSuccessStatusCode)");
        sb.AppendLine("            {");
        sb.AppendLine($"                var dataResult = await response.Content.ReadFromJsonAsync<QueryResult<IEnumerable<{o.EntityName}>>>();");
        sb.AppendLine("");
        sb.AppendLine("                if (dataResult.Result && dataResult.Data != null)");
        sb.AppendLine("                {");
        sb.AppendLine($"                    return new Result<List<{o.EntityName}>>(dataResult.Data.ToList());");
        sb.AppendLine("                }");
        sb.AppendLine("                else");
        sb.AppendLine("                {");
        sb.AppendLine($"                    return new Result<List<{o.EntityName}>>(dataResult.Message);");
        sb.AppendLine("                }");
        sb.AppendLine("            }");
        sb.AppendLine("            else");
        sb.AppendLine($"                return new Result<List<{o.EntityName}>>(response.ReasonPhrase);");
        sb.AppendLine("        }");
        sb.AppendLine("        catch (HttpRequestException reqEx)");
        sb.AppendLine("        {");
        sb.AppendLine($"            return new Result<List<{o.EntityName}>>(reqEx);");
        sb.AppendLine("        }");
        sb.AppendLine("        catch (Exception ex)");
        sb.AppendLine("        {");
        sb.AppendLine($"            return new Result<List<{o.EntityName}>>(ex);");
        sb.AppendLine("        }");
        sb.AppendLine("    }");
        sb.AppendLine("");
        sb.AppendLine($"    public async Task<Result<List<{o.EntityName}>>> Create{o.EntityName}Async({o.EntityName} {ConvertStringToCamelCase(o.EntityName)})");
        sb.AppendLine("    {");
        sb.AppendLine($"        var uri = new Uri(_endpoints.DataServiceBaseUri, $\"{o.TableName.ToLower()}\");");
        sb.AppendLine("");
        sb.AppendLine("        try");
        sb.AppendLine("        {");
        sb.AppendLine($"            var response = await _http.PostAsync(uri, JsonContent.Create({ConvertStringToCamelCase(o.EntityName)}));");
        sb.AppendLine("");
        sb.AppendLine("            if (response.IsSuccessStatusCode)");
        sb.AppendLine("            {");
        sb.AppendLine($"                var dataResult = await response.Content.ReadFromJsonAsync<QueryResult<IEnumerable<{o.EntityName}>>>();");
        sb.AppendLine("");
        sb.AppendLine("                if (dataResult.Result && dataResult.Data != null)");
        sb.AppendLine("                {");
        sb.AppendLine($"                    return new Result<List<{o.EntityName}>>(dataResult.Data.ToList());");
        sb.AppendLine("                }");
        sb.AppendLine("                else");
        sb.AppendLine("                {");
        sb.AppendLine($"                    return new Result<List<{o.EntityName}>>(dataResult.Message);");
        sb.AppendLine("                }");
        sb.AppendLine("            }");
        sb.AppendLine("            else");
        sb.AppendLine($"                return new Result<List<{o.EntityName}>>(response.ReasonPhrase);");
        sb.AppendLine("        }");
        sb.AppendLine("        catch (HttpRequestException reqEx)");
        sb.AppendLine("        {");
        sb.AppendLine($"            return new Result<List<{o.EntityName}>>(reqEx);");
        sb.AppendLine("        }");
        sb.AppendLine("        catch (Exception ex)");
        sb.AppendLine("        {");
        sb.AppendLine($"            return new Result<List<{o.EntityName}>>(ex);");
        sb.AppendLine("        }");
        sb.AppendLine("    }");
        sb.AppendLine("");
        sb.AppendLine($"    public async Task<Result<List<{o.EntityName}>>> Update{o.EntityName}Async({o.EntityName} {ConvertStringToCamelCase(o.EntityName)})");
        sb.AppendLine("    {");
        sb.AppendLine($"        var uri = new Uri(_endpoints.DataServiceBaseUri, $\"{o.TableName.ToLower()}\");");
        sb.AppendLine("");
        sb.AppendLine("        try");
        sb.AppendLine("        {");
        sb.AppendLine("         ");
        sb.AppendLine("");
        sb.AppendLine($"            var response = await _http.PutAsync(uri, JsonContent.Create({ConvertStringToCamelCase(o.EntityName)}));");
        sb.AppendLine("");
        sb.AppendLine("            if (response.IsSuccessStatusCode)");
        sb.AppendLine("            {");
        sb.AppendLine($"                var dataResult = await response.Content.ReadFromJsonAsync<QueryResult<IEnumerable<{o.EntityName}>>>();");
        sb.AppendLine("");
        sb.AppendLine("                if (dataResult.Result && dataResult.Data != null)");
        sb.AppendLine("                {");
        sb.AppendLine($"                    return new Result<List<{o.EntityName}>>(dataResult.Data.ToList());");
        sb.AppendLine("                }");
        sb.AppendLine("                else");
        sb.AppendLine("                {");
        sb.AppendLine($"                    return new Result<List<{o.EntityName}>>(dataResult.Message);");
        sb.AppendLine("                }");
        sb.AppendLine("            }");
        sb.AppendLine("            else");
        sb.AppendLine($"                return new Result<List<{o.EntityName}>>(response.ReasonPhrase);");
        sb.AppendLine("        }");
        sb.AppendLine("        catch (HttpRequestException reqEx)");
        sb.AppendLine("        {");
        sb.AppendLine($"            return new Result<List<{o.EntityName}>>(reqEx);");
        sb.AppendLine("        }");
        sb.AppendLine("        catch (Exception ex)");
        sb.AppendLine("        {");
        sb.AppendLine($"            return new Result<List<{o.EntityName}>>(ex);");
        sb.AppendLine("        }");
        sb.AppendLine("    }");
        sb.AppendLine("");
        sb.AppendLine($"    public async Task<Result<List<{o.EntityName}>>> Delete{o.EntityName}Async({o.EntityName} {ConvertStringToCamelCase(o.EntityName)})");
        sb.AppendLine("    {");
        sb.AppendLine($"        var uri = new Uri(_endpoints.DataServiceBaseUri, $\"{o.TableName.ToLower()}/{{{ConvertStringToCamelCase(o.EntityName)}.{o.PrimaryKey}}}\");");
        sb.AppendLine("");
        sb.AppendLine("        try");
        sb.AppendLine("        {");
        sb.AppendLine("            var response = await _http.DeleteAsync(uri);");
        sb.AppendLine("");
        sb.AppendLine("            if (response.IsSuccessStatusCode)");
        sb.AppendLine("            {");
        sb.AppendLine($"                var dataResult = await response.Content.ReadFromJsonAsync<QueryResult<IEnumerable<{o.EntityName}>>>();");
        sb.AppendLine("");
        sb.AppendLine("                if (dataResult.Result && dataResult.Data != null)");
        sb.AppendLine("                {");
        sb.AppendLine($"                    return new Result<List<{o.EntityName}>>(dataResult.Data.ToList());");
        sb.AppendLine("                }");
        sb.AppendLine("                else");
        sb.AppendLine("                {");
        sb.AppendLine($"                    return new Result<List<{o.EntityName}>>(dataResult.Message);");
        sb.AppendLine("                }");
        sb.AppendLine("            }");
        sb.AppendLine("            else");
        sb.AppendLine($"                return new Result<List<{o.EntityName}>>(response.ReasonPhrase);");
        sb.AppendLine("        }");
        sb.AppendLine("        catch (HttpRequestException reqEx)");
        sb.AppendLine("        {"); 
        sb.AppendLine($"            return new Result<List<{o.EntityName}>>(reqEx);");
        sb.AppendLine("        }");
        sb.AppendLine("        catch (Exception ex)");
        sb.AppendLine("        {");
        sb.AppendLine($"            return new Result<List<{o.EntityName}>>(ex);");
        sb.AppendLine("        }");
        sb.AppendLine("    }");
        sb.AppendLine("}");
        sb.AppendLine("");


        return sb.ToString();
    }
    
    public string GenerateIServicesClassString(ObjectToMap o)
    {
        Console.WriteLine($"Generating Service interface for {o.TableName}");

        var keyFieldName = o.Columns.Find(x => x.ColumnName == o.PrimaryKey).ColumnName;

        var sb = new StringBuilder();
    
        sb.AppendLine($"using {o.UiNameSpace}.Features.Config.Models;");
        sb.AppendLine($"using {o.UiNameSpace}.Shared;");
        sb.AppendLine("");
        sb.AppendLine($"namespace {o.UiNameSpace}.Features.Config.Services;");

        sb.AppendLine("");
        sb.AppendLine($"public interface I{o.EntityName}Service");
        sb.AppendLine("{");
        sb.AppendLine($"    Task<Result<List<{o.EntityName}>>> Get{o.TableName}Async();");
        sb.AppendLine($"    Task<Result<List<{o.EntityName}>>> Create{o.EntityName}Async({o.EntityName} {ConvertStringToCamelCase(o.EntityName)});");
        sb.AppendLine($"    Task<Result<List<{o.EntityName}>>> Update{o.EntityName}Async({o.EntityName} {ConvertStringToCamelCase(o.EntityName)});");
        sb.AppendLine($"    Task<Result<List<{o.EntityName}>>> Delete{o.EntityName}Async({o.EntityName} {ConvertStringToCamelCase(o.EntityName)});");
        sb.AppendLine("}");
        sb.AppendLine("");



        return sb.ToString();
    }


}