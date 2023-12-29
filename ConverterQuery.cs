using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;

public partial class Converter
{
    private string GenerateQueryClassString(string ns, string tableName, string entityName, 
                    List<ColumnDefinition> columns, string primaryKey)
    {
        Console.WriteLine($"Generating query class for {tableName}");

        var sb = new StringBuilder();

        sb.AppendLine("using DataService.Shared.Entities;");
        sb.AppendLine("using DataService.Config.Features.Translations.Entities;");
        sb.AppendLine("using DataService.Config.Features.Translations.Shared;");
        sb.AppendLine("using AuditTrail.Core;");
        sb.AppendLine("using MediatR;");
        sb.AppendLine("using Microsoft.Extensions.Logging;");
        sb.AppendLine("using DataService.Shared.Extensions;");

        sb.AppendLine("");
        sb.AppendLine($"namespace {ns}.Queries;");
        sb.AppendLine("");
        sb.AppendLine($"public record Read{tableName} : IRequest<QueryResult<IEnumerable<{entityName}>>>");
        sb.AppendLine("{");
        sb.AppendLine($"    public sealed record Query() : IRequest<QueryResult<IEnumerable<{entityName}>>>");
        sb.AppendLine("    {");
        sb.AppendLine($"        public string? {primaryKey} {{ get; set; }} = null;");
        sb.AppendLine("    }"); 
        sb.AppendLine("");
        sb.AppendLine($"    internal class Handler : IRequestHandler<Query, QueryResult<IEnumerable<{entityName}>>>");
        sb.AppendLine("    {");
        sb.AppendLine($"        private readonly I{tableName}Repository _repo;");
        sb.AppendLine($"        private readonly ILogger<Handler> _logger;");
        sb.AppendLine("");
        sb.AppendLine($"        public Handler(I{tableName}Repository repository, ILogger<Handler> logger)");
        sb.AppendLine("        {");
        sb.AppendLine($"            _repo = repository;");
        sb.AppendLine($"            _logger = logger;");
        sb.AppendLine("        }");
        sb.AppendLine("");
        sb.AppendLine($"        public async Task<QueryResult<IEnumerable<{entityName}>>> Handle(Query request, CancellationToken cancellationToken)");
        sb.AppendLine("        {");
        sb.AppendLine($"            _logger.LogDebug(\"Entered handler in {{@Class}} with request {{@Request}}\", nameof(Read{tableName}), request);");
        sb.AppendLine("");
        sb.AppendLine($"            return await _repo.ReadAsync(request.{primaryKey});");
        sb.AppendLine("        }");
        sb.AppendLine("    }");
        sb.AppendLine("}");
        sb.AppendLine("");

        sb.AppendLine($"public record Update{tableName} : IRequest<QueryResult<IEnumerable<{entityName}>>>");
        sb.AppendLine("{");
        sb.AppendLine($"    public sealed record Update{entityName}Command : IRequest<QueryResult<IEnumerable<{entityName}>>>");
        sb.AppendLine("    {");
        sb.AppendLine($"        public {entityName} {entityName} {{ get; set; }}");
        sb.AppendLine("         public AuditInformation AuditInformation { get; set; }");
        sb.AppendLine("    }"); 
        sb.AppendLine("");
        sb.AppendLine($"    internal class Handler : IRequestHandler<Update{entityName}Command, QueryResult<IEnumerable<{entityName}>>>");
        sb.AppendLine("    {");
        sb.AppendLine($"        private readonly I{tableName}Repository _repo;");
        sb.AppendLine($"        private readonly ILogger<Handler> _logger;");
        sb.AppendLine($"        private readonly IAuditTrailer _auditTrailer;");
        sb.AppendLine("");
        sb.AppendLine($"        public Handler(I{tableName}Repository repository, ILogger<Handler> logger, IAuditTrailer auditTrailer)");
        sb.AppendLine("        {");
        sb.AppendLine($"            _repo = repository;");
        sb.AppendLine($"            _logger = logger;");
        sb.AppendLine($"            _auditTrailer = auditTrailer;");
        sb.AppendLine("        }");
        sb.AppendLine("");
        sb.AppendLine($"        public async Task<QueryResult<IEnumerable<{entityName}>>> Handle(Update{entityName}Command request, CancellationToken cancellationToken)");
        sb.AppendLine("        {");
        sb.AppendLine($"            _logger.LogDebug(\"Entered handler in {{@Class}} with request {{@Request}}\", nameof(Update{tableName}), request);");
        sb.AppendLine("");
        sb.AppendLine("             // update audit trail");
        sb.AppendLine($"            var oldObjectResult = await _repo.ReadAsync(request.{entityName}.{primaryKey});");
        sb.AppendLine($"            var oldObject = oldObjectResult?.Data?.FirstOrDefault();");
        sb.AppendLine("");
        sb.AppendLine($"            var result =  await _repo.UpdateAsync(request.{entityName});");
        sb.AppendLine("");
        sb.AppendLine($"            if (result.Result)");
        sb.AppendLine("            {");
        sb.AppendLine($"                if (request.AuditInformation == null)");
        sb.AppendLine("                {");
        sb.AppendLine($"                    request.AuditInformation = new AuditInformation(\"unknown\",\"unknown\");");
        sb.AppendLine("                }");
        sb.AppendLine($"                await _auditTrailer.LogUpdate(request.AuditInformation.Application, request.AuditInformation.Username, oldObject, request.{entityName}, typeof({entityName}).ToString(), \"NA\");");    
        sb.AppendLine($"                await _auditTrailer.Commit();");
        sb.AppendLine("            }");
        sb.AppendLine("");
        sb.AppendLine($"            return result;");
        sb.AppendLine("        }");
        sb.AppendLine("    }");
        sb.AppendLine("}");
        sb.AppendLine("");

        sb.AppendLine($"public record Delete{tableName} : IRequest<QueryResult<IEnumerable<{entityName}>>>");
        sb.AppendLine("{");
        sb.AppendLine($"    public sealed record Delete{entityName}Command : IRequest<QueryResult<IEnumerable<{entityName}>>>");
        sb.AppendLine("    {");
        sb.AppendLine($"        public string {primaryKey} {{ get; set; }}");
        sb.AppendLine("         public AuditInformation AuditInformation { get; set; }");
        sb.AppendLine("    }");
        sb.AppendLine("");
        sb.AppendLine($"    internal class Handler : IRequestHandler<Delete{entityName}Command, QueryResult<IEnumerable<{entityName}>>>");
        sb.AppendLine("    {");
        sb.AppendLine($"        private readonly I{tableName}Repository _repo;");
        sb.AppendLine($"        private readonly ILogger<Handler> _logger;");
        sb.AppendLine($"        private readonly IAuditTrailer _auditTrailer;");
        sb.AppendLine("");
        sb.AppendLine($"        public Handler(I{tableName}Repository repository, ILogger<Handler> logger, IAuditTrailer auditTrailer)");
        sb.AppendLine("        {");
        sb.AppendLine($"            _repo = repository;");
        sb.AppendLine($"            _logger = logger;");
        sb.AppendLine($"            _auditTrailer = auditTrailer;");
        sb.AppendLine("        }");
        sb.AppendLine("");
        sb.AppendLine($"        public async Task<QueryResult<IEnumerable<{entityName}>>> Handle(Delete{entityName}Command request, CancellationToken cancellationToken)");
        sb.AppendLine("        {");
        sb.AppendLine($"            _logger.LogDebug(\"Entered handler in {{@Class}} with request {{@Request}}\", nameof(Delete{tableName}), request);");
        sb.AppendLine("             // update audit trail");
        sb.AppendLine($"            var oldObjectResult = await _repo.ReadAsync(request.{primaryKey});");
        sb.AppendLine($"            var oldObject = oldObjectResult?.Data?.FirstOrDefault();");
        sb.AppendLine("");
        sb.AppendLine($"            var result =  await _repo.DeleteAsync(request.{primaryKey});");
        sb.AppendLine("");
        sb.AppendLine($"            if (result.Result)");
        sb.AppendLine("            {");
        sb.AppendLine($"                if (request.AuditInformation == null)");
        sb.AppendLine("                {");
        sb.AppendLine($"                    request.AuditInformation = new AuditInformation(\"unknown\",\"unknown\");");
        sb.AppendLine("                }");
        sb.AppendLine($"                await _auditTrailer.LogDelete(request.AuditInformation.Application, request.AuditInformation.Username, oldObject, typeof({entityName}).ToString(), \"NA\");");    
        sb.AppendLine($"                await _auditTrailer.Commit();");
        sb.AppendLine("            }");
        sb.AppendLine("");
        sb.AppendLine($"            return result;");
        sb.AppendLine("        }");
        sb.AppendLine("    }");
        sb.AppendLine("}");
        sb.AppendLine("");


        sb.AppendLine($"public record Create{tableName} : IRequest<QueryResult<IEnumerable<{entityName}>>>");
        sb.AppendLine("{");
        sb.AppendLine($"    internal record Create{entityName}Command : IRequest<QueryResult<IEnumerable<{entityName}>>>");

        sb.AppendLine("     {");
        sb.AppendLine($"        public {entityName} {entityName} {{ get; set; }}");
        sb.AppendLine("         public AuditInformation AuditInformation { get; set; }");
        sb.AppendLine("     }");
        sb.AppendLine("");
        sb.AppendLine($"    internal class Handler : IRequestHandler<Create{entityName}Command, QueryResult<IEnumerable<{entityName}>>>");
        sb.AppendLine("    {");
        sb.AppendLine($"        private readonly I{tableName}Repository _repo;");
        sb.AppendLine($"        private readonly ILogger<Handler> _logger;");
        sb.AppendLine($"        private readonly IAuditTrailer _auditTrailer;");
        sb.AppendLine("");
        sb.AppendLine($"        public Handler(I{tableName}Repository repository, ILogger<Handler> logger, IAuditTrailer auditTrailer)");
        sb.AppendLine("        {");
        sb.AppendLine($"            _repo = repository;");
        sb.AppendLine($"            _logger = logger;");
        sb.AppendLine($"            _auditTrailer = auditTrailer;");
        sb.AppendLine("        }");
        sb.AppendLine("");
        sb.AppendLine($"        public async Task<QueryResult<IEnumerable<{entityName}>>> Handle(Create{entityName}Command request, CancellationToken cancellationToken)");
        sb.AppendLine("        {");
        sb.AppendLine($"            _logger.LogDebug(\"Entered handler in {{@Class}} with request {{@Request}}\", nameof(Create{tableName}), request);");
        sb.AppendLine("");
        sb.AppendLine($"            var result =  await _repo.CreateAsync(request.{entityName});");
        sb.AppendLine("");
        sb.AppendLine($"            if (result.Result)");
        sb.AppendLine("            {");
        sb.AppendLine($"                if (request.AuditInformation == null)");
        sb.AppendLine("                {");
        sb.AppendLine($"                    request.AuditInformation = new AuditInformation(\"unknown\",\"unknown\");");
        sb.AppendLine("                }");
        sb.AppendLine($"                await _auditTrailer.LogCreate(request.AuditInformation.Application, request.AuditInformation.Username, request.{entityName}, typeof({entityName}).ToString(), \"NA\");");    
        sb.AppendLine($"                await _auditTrailer.Commit();");
        sb.AppendLine("            }");
        sb.AppendLine("");
        sb.AppendLine($"            return result;");
        sb.AppendLine("        }");
        sb.AppendLine("    }");
        sb.AppendLine("}");


        

        return sb.ToString();
    }
}