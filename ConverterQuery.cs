using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;

public partial class Converter
{
    public string GenerateQueryClassString(ObjectToMap o)
    {
        Console.WriteLine($"Generating query class for {o.TableName}");

        var sb = new StringBuilder();

        var primaryKeyType = o.Columns.Find(x => x.ColumnName == o.PrimaryKey).DataTypeToCSharpType();  

        sb.AppendLine("using DataService.Shared.Entities;");
        sb.AppendLine($"using DataService.Config.Features.{o.FolderLevel1}.Entities;");
        sb.AppendLine($"using DataService.Config.Features.{o.FolderLevel1}.Shared;");
        sb.AppendLine("using AuditTrail.Core;");
        sb.AppendLine("using MediatR;");
        sb.AppendLine("using Microsoft.Extensions.Logging;");
        sb.AppendLine("using DataService.Shared.Extensions;");

        sb.AppendLine("");
        sb.AppendLine($"namespace {o.NameSpace}.Queries;");
        sb.AppendLine("");
        sb.AppendLine($"public record Read{o.TableName} : IRequest<QueryResult<IEnumerable<{o.EntityName}>>>");
        sb.AppendLine("{");
        sb.AppendLine($"    public sealed record Query() : IRequest<QueryResult<IEnumerable<{o.EntityName}>>>");
        sb.AppendLine("    {");

        sb.AppendLine($"        public {primaryKeyType}? {o.PrimaryKey} {{ get; set; }} = null;");
        sb.AppendLine("    }"); 
        sb.AppendLine("");
        sb.AppendLine($"    internal class Handler : IRequestHandler<Query, QueryResult<IEnumerable<{o.EntityName}>>>");
        sb.AppendLine("    {");
        sb.AppendLine($"        private readonly I{o.TableName}Repository _repo;");
        sb.AppendLine($"        private readonly ILogger<Handler> _logger;");
        sb.AppendLine("");
        sb.AppendLine($"        public Handler(I{o.TableName}Repository repository, ILogger<Handler> logger)");
        sb.AppendLine("        {");
        sb.AppendLine($"            _repo = repository;");
        sb.AppendLine($"            _logger = logger;");
        sb.AppendLine("        }");
        sb.AppendLine("");
        sb.AppendLine($"        public async Task<QueryResult<IEnumerable<{o.EntityName}>>> Handle(Query request, CancellationToken cancellationToken)");
        sb.AppendLine("        {");
        sb.AppendLine($"            _logger.LogDebug(\"Entered handler in {{@Class}} with request {{@Request}}\", nameof(Read{o.TableName}), request);");
        sb.AppendLine("");
        sb.AppendLine($"            return await _repo.ReadAsync(request.{o.PrimaryKey});");
        sb.AppendLine("        }");
        sb.AppendLine("    }");
        sb.AppendLine("}");
        sb.AppendLine("");

        sb.AppendLine($"public record Update{o.TableName} : IRequest<QueryResult<IEnumerable<{o.EntityName}>>>");
        sb.AppendLine("{");
        sb.AppendLine($"    public sealed record Update{o.EntityName}Command : IRequest<QueryResult<IEnumerable<{o.EntityName}>>>");
        sb.AppendLine("    {");
        sb.AppendLine($"        public {o.EntityName} {o.EntityName} {{ get; set; }}");
        sb.AppendLine("         public AuditInformation AuditInformation { get; set; }");
        sb.AppendLine("    }"); 
        sb.AppendLine("");
        sb.AppendLine($"    internal class Handler : IRequestHandler<Update{o.EntityName}Command, QueryResult<IEnumerable<{o.EntityName}>>>");
        sb.AppendLine("    {");
        sb.AppendLine($"        private readonly I{o.TableName}Repository _repo;");
        sb.AppendLine($"        private readonly ILogger<Handler> _logger;");
        sb.AppendLine($"        private readonly IAuditTrailer _auditTrailer;");
        sb.AppendLine("");
        sb.AppendLine($"        public Handler(I{o.TableName}Repository repository, ILogger<Handler> logger, IAuditTrailer auditTrailer)");
        sb.AppendLine("        {");
        sb.AppendLine($"            _repo = repository;");
        sb.AppendLine($"            _logger = logger;");
        sb.AppendLine($"            _auditTrailer = auditTrailer;");
        sb.AppendLine("        }");
        sb.AppendLine("");
        sb.AppendLine($"        public async Task<QueryResult<IEnumerable<{o.EntityName}>>> Handle(Update{o.EntityName}Command request, CancellationToken cancellationToken)");
        sb.AppendLine("        {");
        sb.AppendLine($"            _logger.LogDebug(\"Entered handler in {{@Class}} with request {{@Request}}\", nameof(Update{o.TableName}), request);");
        sb.AppendLine("");
        sb.AppendLine("             // update audit trail");
        sb.AppendLine($"            var oldObjectResult = await _repo.ReadAsync(request.{o.EntityName}.{o.PrimaryKey});");
        sb.AppendLine($"            var oldObject = oldObjectResult?.Data?.FirstOrDefault();");
        sb.AppendLine("");
        sb.AppendLine($"            var result =  await _repo.UpdateAsync(request.{o.EntityName});");
        sb.AppendLine("");
        sb.AppendLine($"            if (result.Result)");
        sb.AppendLine("            {");
        sb.AppendLine($"                if (request.AuditInformation == null)");
        sb.AppendLine("                {");
        sb.AppendLine($"                    request.AuditInformation = new AuditInformation(\"unknown\",\"unknown\");");
        sb.AppendLine("                }");
        sb.AppendLine($"                await _auditTrailer.LogUpdate(request.AuditInformation.Application, request.AuditInformation.Username, oldObject, request.{o.EntityName}, typeof({o.EntityName}).ToString(), \"NA\");");    
        sb.AppendLine($"                await _auditTrailer.Commit();");
        sb.AppendLine("            }");
        sb.AppendLine("");
        sb.AppendLine($"            return result;");
        sb.AppendLine("        }");
        sb.AppendLine("    }");
        sb.AppendLine("}");
        sb.AppendLine("");

        sb.AppendLine($"public record Delete{o.TableName} : IRequest<QueryResult<IEnumerable<{o.EntityName}>>>");
        sb.AppendLine("{");
        sb.AppendLine($"    public sealed record Delete{o.EntityName}Command : IRequest<QueryResult<IEnumerable<{o.EntityName}>>>");
        sb.AppendLine("    {");


        sb.AppendLine($"        public {primaryKeyType} {o.PrimaryKey} {{ get; set; }}");
        sb.AppendLine("         public AuditInformation AuditInformation { get; set; }");
        sb.AppendLine("    }");
        sb.AppendLine("");
        sb.AppendLine($"    internal class Handler : IRequestHandler<Delete{o.EntityName}Command, QueryResult<IEnumerable<{o.EntityName}>>>");
        sb.AppendLine("    {");
        sb.AppendLine($"        private readonly I{o.TableName}Repository _repo;");
        sb.AppendLine($"        private readonly ILogger<Handler> _logger;");
        sb.AppendLine($"        private readonly IAuditTrailer _auditTrailer;");
        sb.AppendLine("");
        sb.AppendLine($"        public Handler(I{o.TableName}Repository repository, ILogger<Handler> logger, IAuditTrailer auditTrailer)");
        sb.AppendLine("        {");
        sb.AppendLine($"            _repo = repository;");
        sb.AppendLine($"            _logger = logger;");
        sb.AppendLine($"            _auditTrailer = auditTrailer;");
        sb.AppendLine("        }");
        sb.AppendLine("");
        sb.AppendLine($"        public async Task<QueryResult<IEnumerable<{o.EntityName}>>> Handle(Delete{o.EntityName}Command request, CancellationToken cancellationToken)");
        sb.AppendLine("        {");
        sb.AppendLine($"            _logger.LogDebug(\"Entered handler in {{@Class}} with request {{@Request}}\", nameof(Delete{o.TableName}), request);");
        sb.AppendLine("             // update audit trail");
        sb.AppendLine($"            var oldObjectResult = await _repo.ReadAsync(request.{o.PrimaryKey});");
        sb.AppendLine($"            var oldObject = oldObjectResult?.Data?.FirstOrDefault();");
        sb.AppendLine("");
        sb.AppendLine($"            var result =  await _repo.DeleteAsync(request.{o.PrimaryKey});");
        sb.AppendLine("");
        sb.AppendLine($"            if (result.Result)");
        sb.AppendLine("            {");
        sb.AppendLine($"                if (request.AuditInformation == null)");
        sb.AppendLine("                {");
        sb.AppendLine($"                    request.AuditInformation = new AuditInformation(\"unknown\",\"unknown\");");
        sb.AppendLine("                }");
        sb.AppendLine($"                await _auditTrailer.LogDelete(request.AuditInformation.Application, request.AuditInformation.Username, oldObject, typeof({o.EntityName}).ToString(), \"NA\");");    
        sb.AppendLine($"                await _auditTrailer.Commit();");
        sb.AppendLine("            }");
        sb.AppendLine("");
        sb.AppendLine($"            return result;");
        sb.AppendLine("        }");
        sb.AppendLine("    }");
        sb.AppendLine("}");
        sb.AppendLine("");


        sb.AppendLine($"public record Create{o.TableName} : IRequest<QueryResult<IEnumerable<{o.EntityName}>>>");
        sb.AppendLine("{");
        sb.AppendLine($"    internal record Create{o.EntityName}Command : IRequest<QueryResult<IEnumerable<{o.EntityName}>>>");

        sb.AppendLine("     {");
        sb.AppendLine($"        public {o.EntityName} {o.EntityName} {{ get; set; }}");
        sb.AppendLine("         public AuditInformation AuditInformation { get; set; }");
        sb.AppendLine("     }");
        sb.AppendLine("");
        sb.AppendLine($"    internal class Handler : IRequestHandler<Create{o.EntityName}Command, QueryResult<IEnumerable<{o.EntityName}>>>");
        sb.AppendLine("    {");
        sb.AppendLine($"        private readonly I{o.TableName}Repository _repo;");
        sb.AppendLine($"        private readonly ILogger<Handler> _logger;");
        sb.AppendLine($"        private readonly IAuditTrailer _auditTrailer;");
        sb.AppendLine("");
        sb.AppendLine($"        public Handler(I{o.TableName}Repository repository, ILogger<Handler> logger, IAuditTrailer auditTrailer)");
        sb.AppendLine("        {");
        sb.AppendLine($"            _repo = repository;");
        sb.AppendLine($"            _logger = logger;");
        sb.AppendLine($"            _auditTrailer = auditTrailer;");
        sb.AppendLine("        }");
        sb.AppendLine("");
        sb.AppendLine($"        public async Task<QueryResult<IEnumerable<{o.EntityName}>>> Handle(Create{o.EntityName}Command request, CancellationToken cancellationToken)");
        sb.AppendLine("        {");
        sb.AppendLine($"            _logger.LogDebug(\"Entered handler in {{@Class}} with request {{@Request}}\", nameof(Create{o.TableName}), request);");
        sb.AppendLine("");
        sb.AppendLine($"            var result =  await _repo.CreateAsync(request.{o.EntityName});");
        sb.AppendLine("");
        sb.AppendLine($"            if (result.Result)");
        sb.AppendLine("            {");
        sb.AppendLine($"                if (request.AuditInformation == null)");
        sb.AppendLine("                {");
        sb.AppendLine($"                    request.AuditInformation = new AuditInformation(\"unknown\",\"unknown\");");
        sb.AppendLine("                }");
        sb.AppendLine($"                await _auditTrailer.LogCreate(request.AuditInformation.Application, request.AuditInformation.Username, request.{o.EntityName}, typeof({o.EntityName}).ToString(), \"NA\");");    
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