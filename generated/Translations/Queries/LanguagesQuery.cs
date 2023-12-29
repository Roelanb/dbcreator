using DataService.Shared.Entities;
using DataService.Config.Features.Translations.Entities;
using DataService.Config.Features.Translations.Shared;
using AuditTrail.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using DataService.Shared.Extensions;

namespace DataService.Config.Features.Translations.Queries;

public record ReadLanguages : IRequest<QueryResult<IEnumerable<Language>>>
{
    public sealed record Query() : IRequest<QueryResult<IEnumerable<Language>>>
    {
        public string? Name { get; set; } = null;
    }

    internal class Handler : IRequestHandler<Query, QueryResult<IEnumerable<Language>>>
    {
        private readonly ILanguagesRepository _repo;
        private readonly ILogger<Handler> _logger;

        public Handler(ILanguagesRepository repository, ILogger<Handler> logger)
        {
            _repo = repository;
            _logger = logger;
        }

        public async Task<QueryResult<IEnumerable<Language>>> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Entered handler in {@Class} with request {@Request}", nameof(ReadLanguages), request);

            return await _repo.ReadAsync(request.Name);
        }
    }
}

public record UpdateLanguages : IRequest<QueryResult<IEnumerable<Language>>>
{
    public sealed record UpdateLanguageCommand : IRequest<QueryResult<IEnumerable<Language>>>
    {
        public Language Language { get; set; }
         public AuditInformation AuditInformation { get; set; }
    }

    internal class Handler : IRequestHandler<UpdateLanguageCommand, QueryResult<IEnumerable<Language>>>
    {
        private readonly ILanguagesRepository _repo;
        private readonly ILogger<Handler> _logger;
        private readonly IAuditTrailer _auditTrailer;

        public Handler(ILanguagesRepository repository, ILogger<Handler> logger, IAuditTrailer auditTrailer)
        {
            _repo = repository;
            _logger = logger;
            _auditTrailer = auditTrailer;
        }

        public async Task<QueryResult<IEnumerable<Language>>> Handle(UpdateLanguageCommand request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Entered handler in {@Class} with request {@Request}", nameof(UpdateLanguages), request);

             // update audit trail
            var oldObjectResult = await _repo.ReadAsync(request.Language.Name);
            var oldObject = oldObjectResult?.Data?.FirstOrDefault();

            var result =  await _repo.UpdateAsync(request.Language);

            if (result.Result)
            {
                if (request.AuditInformation == null)
                {
                    request.AuditInformation = new AuditInformation("unknown","unknown");
                }
                await _auditTrailer.LogUpdate(request.AuditInformation.Application, request.AuditInformation.Username, oldObject, request.Language, typeof(Language).ToString(), "NA");
                await _auditTrailer.Commit();
            }

            return result;
        }
    }
}

public record DeleteLanguages : IRequest<QueryResult<IEnumerable<Language>>>
{
    public sealed record DeleteLanguageCommand : IRequest<QueryResult<IEnumerable<Language>>>
    {
        public string Name { get; set; }
         public AuditInformation AuditInformation { get; set; }
    }

    internal class Handler : IRequestHandler<DeleteLanguageCommand, QueryResult<IEnumerable<Language>>>
    {
        private readonly ILanguagesRepository _repo;
        private readonly ILogger<Handler> _logger;
        private readonly IAuditTrailer _auditTrailer;

        public Handler(ILanguagesRepository repository, ILogger<Handler> logger, IAuditTrailer auditTrailer)
        {
            _repo = repository;
            _logger = logger;
            _auditTrailer = auditTrailer;
        }

        public async Task<QueryResult<IEnumerable<Language>>> Handle(DeleteLanguageCommand request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Entered handler in {@Class} with request {@Request}", nameof(DeleteLanguages), request);
             // update audit trail
            var oldObjectResult = await _repo.ReadAsync(request.Name);
            var oldObject = oldObjectResult?.Data?.FirstOrDefault();

            var result =  await _repo.DeleteAsync(request.Name);

            if (result.Result)
            {
                if (request.AuditInformation == null)
                {
                    request.AuditInformation = new AuditInformation("unknown","unknown");
                }
                await _auditTrailer.LogDelete(request.AuditInformation.Application, request.AuditInformation.Username, oldObject, typeof(Language).ToString(), "NA");
                await _auditTrailer.Commit();
            }

            return result;
        }
    }
}

public record CreateLanguages : IRequest<QueryResult<IEnumerable<Language>>>
{
    internal record CreateLanguageCommand : IRequest<QueryResult<IEnumerable<Language>>>
     {
        public Language Language { get; set; }
         public AuditInformation AuditInformation { get; set; }
     }

    internal class Handler : IRequestHandler<CreateLanguageCommand, QueryResult<IEnumerable<Language>>>
    {
        private readonly ILanguagesRepository _repo;
        private readonly ILogger<Handler> _logger;
        private readonly IAuditTrailer _auditTrailer;

        public Handler(ILanguagesRepository repository, ILogger<Handler> logger, IAuditTrailer auditTrailer)
        {
            _repo = repository;
            _logger = logger;
            _auditTrailer = auditTrailer;
        }

        public async Task<QueryResult<IEnumerable<Language>>> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Entered handler in {@Class} with request {@Request}", nameof(CreateLanguages), request);

            var result =  await _repo.CreateAsync(request.Language);

            if (result.Result)
            {
                if (request.AuditInformation == null)
                {
                    request.AuditInformation = new AuditInformation("unknown","unknown");
                }
                await _auditTrailer.LogCreate(request.AuditInformation.Application, request.AuditInformation.Username, request.Language, typeof(Language).ToString(), "NA");
                await _auditTrailer.Commit();
            }

            return result;
        }
    }
}
