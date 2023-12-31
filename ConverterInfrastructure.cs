using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;

public partial class Converter
{
    public string GenerateApiInfrastructureString(List<ObjectToMap> objectsToMap)
    {
        Console.WriteLine($"Generating API interface ServiceCollectionExtensions class");

        var sb = new StringBuilder();

        foreach (var o in objectsToMap.DistinctBy(x => x.FolderLevel1))
        {
            sb.AppendLine($"using DataService.Config.Features.{o.FolderLevel1}.Shared;");
        }
        sb.AppendLine("using Microsoft.Extensions.Configuration;");
        sb.AppendLine("using Microsoft.Extensions.DependencyInjection;");
        sb.AppendLine("");
        sb.AppendLine("namespace DataService.Config.Infrastructure;");
        sb.AppendLine("");
        sb.AppendLine("public static class ServiceCollectionExtensions");
        sb.AppendLine("{");
        sb.AppendLine("    public static IServiceCollection AddConfigApplication(this IServiceCollection services, IConfiguration configuration)");
        sb.AppendLine("    {");
        sb.AppendLine("        return services");
        sb.AppendLine("            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AssemblyTarget).Assembly))");
        sb.AppendLine("            .AddRepositories();");
        sb.AppendLine("    }");
        sb.AppendLine("");
        sb.AppendLine("    private static IServiceCollection AddRepositories(this IServiceCollection services)");
        sb.AppendLine("    {");
        sb.AppendLine("        return services");
        foreach (var o in objectsToMap)
        {
            sb.AppendLine($"            .AddScoped<I{o.TableName}Repository, {o.TableName}Repository>()");
        }
        sb.Remove(sb.Length - 2, 2);
        sb.AppendLine(";");
        sb.AppendLine("    }");
        sb.AppendLine("    }");



        return sb.ToString();
    }

    public string GenerateFrontEndServiceDeclarationString(List<ObjectToMap> objectsToMap)
    {
        Console.WriteLine($"Generating UI Program fileclass");

        var sb = new StringBuilder();

    
        foreach (var o in objectsToMap)
        {
            sb.AppendLine($"builder.Services.AddScoped<I{o.EntityName}Service, {o.EntityName}Service>();");
        }


        return sb.ToString();
    }
}