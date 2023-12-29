using DataService.Shared.Entities;
using DataService.Config.Features.Translations.Entities;
using DataService.Config.Features.Translations.Shared;
using AuditTrail.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using DataService.Shared.Extensions;

namespace DataService.Config.Features.Translations.Queries;

public record ReadTranslationsTypes : IRequest<QueryResult<IEnumerable<TranslationsType>>>
{
    public sealed record Query() : IRequest<QueryResult<IEnumerable<TranslationsType>>>
    {
        public string? translation_type { get; set; } = null;
    }

    internal class Handler : IRequestHandler<Query, QueryResult<IEnumerable<TranslationsType>>>
    {
        private readonly ITranslationsTypesRepository _repo;
        private readonly ILogger<Handler> _logger;

        public Handler(ITranslationsTypesRepository repository, ILogger<Handler> logger)
        {
            _repo = repository;
            _logger = logger;
        }

        public async Task<QueryResult<IEnumerable<TranslationsType>>> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Entered handler in {@Class} with request {@Request}", nameof(ReadTranslationsTypes), request);

            return await _repo.ReadAsync(request.translation_type);
        }
    }
}

public record UpdateTranslationsTypes : IRequest<QueryResult<IEnumerable<TranslationsType>>>
{
    public sealed record UpdateTranslationsTypeCommand : IRequest<QueryResult<IEnumerable<TranslationsType>>>
    {
        public TranslationsType TranslationsType { get; set; }
         public AuditInformation AuditInformation { get; set; }
    }

    internal class Handler : IRequestHandler<UpdateTranslationsTypeCommand, QueryResult<IEnumerable<TranslationsType>>>
    {
        private readonly ITranslationsTypesRepository _repo;
        private readonly ILogger<Handler> _logger;
        private readonly IAuditTrailer _auditTrailer;

        public Handler(ITranslationsTypesRepository repository, ILogger<Handler> logger, IAuditTrailer auditTrailer)
        {
            _repo = repository;
            _logger = logger;
            _auditTrailer = auditTrailer;
        }

        public async Task<QueryResult<IEnumerable<TranslationsType>>> Handle(UpdateTranslationsTypeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Entered handler in {@Class} with request {@Request}", nameof(UpdateTranslationsTypes), request);

             // update audit trail
            var oldObjectResult = await _repo.ReadAsync(request.TranslationsType.translation_type);
            var oldObject = oldObjectResult?.Data?.FirstOrDefault();

            var result =  await _repo.UpdateAsync(request.TranslationsType);

            if (result.Result)
            {
                if (request.AuditInformation == null)
                {
                    request.AuditInformation = new AuditInformation("unknown","unknown");
                }
                await _auditTrailer.LogUpdate(request.AuditInformation.Application, request.AuditInformation.Username, oldObject, request.TranslationsType, typeof(TranslationsType).ToString(), "NA");
                await _auditTrailer.Commit();
            }

            return result;
        }
    }
}

public record DeleteTranslationsTypes : IRequest<QueryResult<IEnumerable<TranslationsType>>>
{
    public sealed record DeleteTranslationsTypeCommand : IRequest<QueryResult<IEnumerable<TranslationsType>>>
    {
        public string translation_type { get; set; }
         public AuditInformation AuditInformation { get; set; }
    }

    internal class Handler : IRequestHandler<DeleteTranslationsTypeCommand, QueryResult<IEnumerable<TranslationsType>>>
    {
        private readonly ITranslationsTypesRepository _repo;
        private readonly ILogger<Handler> _logger;
        private readonly IAuditTrailer _auditTrailer;

        public Handler(ITranslationsTypesRepository repository, ILogger<Handler> logger, IAuditTrailer auditTrailer)
        {
            _repo = repository;
            _logger = logger;
            _auditTrailer = auditTrailer;
        }

        public async Task<QueryResult<IEnumerable<TranslationsType>>> Handle(DeleteTranslationsTypeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Entered handler in {@Class} with request {@Request}", nameof(DeleteTranslationsTypes), request);
             // update audit trail
            var oldObjectResult = await _repo.ReadAsync(request.translation_type);
            var oldObject = oldObjectResult?.Data?.FirstOrDefault();

            var result =  await _repo.DeleteAsync(request.translation_type);

            if (result.Result)
            {
                if (request.AuditInformation == null)
                {
                    request.AuditInformation = new AuditInformation("unknown","unknown");
                }
                await _auditTrailer.LogDelete(request.AuditInformation.Application, request.AuditInformation.Username, oldObject, typeof(TranslationsType).ToString(), "NA");
                await _auditTrailer.Commit();
            }

            return result;
        }
    }
}

public record CreateTranslationsTypes : IRequest<QueryResult<IEnumerable<TranslationsType>>>
{
    internal record CreateTranslationsTypeCommand : IRequest<QueryResult<IEnumerable<TranslationsType>>>
     {
        public TranslationsType TranslationsType { get; set; }
         public AuditInformation AuditInformation { get; set; }
     }

    internal class Handler : IRequestHandler<CreateTranslationsTypeCommand, QueryResult<IEnumerable<TranslationsType>>>
    {
        private readonly ITranslationsTypesRepository _repo;
        private readonly ILogger<Handler> _logger;
        private readonly IAuditTrailer _auditTrailer;

        public Handler(ITranslationsTypesRepository repository, ILogger<Handler> logger, IAuditTrailer auditTrailer)
        {
            _repo = repository;
            _logger = logger;
            _auditTrailer = auditTrailer;
        }

        public async Task<QueryResult<IEnumerable<TranslationsType>>> Handle(CreateTranslationsTypeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Entered handler in {@Class} with request {@Request}", nameof(CreateTranslationsTypes), request);

            var result =  await _repo.CreateAsync(request.TranslationsType);

            if (result.Result)
            {
                if (request.AuditInformation == null)
                {
                    request.AuditInformation = new AuditInformation("unknown","unknown");
                }
                await _auditTrailer.LogCreate(request.AuditInformation.Application, request.AuditInformation.Username, request.TranslationsType, typeof(TranslationsType).ToString(), "NA");
                await _auditTrailer.Commit();
            }

            return result;
        }
    }
}
