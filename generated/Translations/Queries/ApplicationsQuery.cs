using DataService.Shared.Entities;
using DataService.Config.Features.Translations.Entities;
using DataService.Config.Features.Translations.Shared;
using AuditTrail.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using DataService.Shared.Extensions;

namespace DataService.Config.Features.Translations.Queries;

public record ReadApplications : IRequest<QueryResult<IEnumerable<Application>>>
{
    public sealed record Query() : IRequest<QueryResult<IEnumerable<Application>>>
    {
        public string? Name { get; set; } = null;
    }

    internal class Handler : IRequestHandler<Query, QueryResult<IEnumerable<Application>>>
    {
        private readonly IApplicationsRepository _repo;
        private readonly ILogger<Handler> _logger;

        public Handler(IApplicationsRepository repository, ILogger<Handler> logger)
        {
            _repo = repository;
            _logger = logger;
        }

        public async Task<QueryResult<IEnumerable<Application>>> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Entered handler in {@Class} with request {@Request}", nameof(ReadApplications), request);

            return await _repo.ReadAsync(request.Name);
        }
    }
}

public record UpdateApplications : IRequest<QueryResult<IEnumerable<Application>>>
{
    public sealed record UpdateApplicationCommand : IRequest<QueryResult<IEnumerable<Application>>>
    {
        public Application Application { get; set; }
         public AuditInformation AuditInformation { get; set; }
    }

    internal class Handler : IRequestHandler<UpdateApplicationCommand, QueryResult<IEnumerable<Application>>>
    {
        private readonly IApplicationsRepository _repo;
        private readonly ILogger<Handler> _logger;
        private readonly IAuditTrailer _auditTrailer;

        public Handler(IApplicationsRepository repository, ILogger<Handler> logger, IAuditTrailer auditTrailer)
        {
            _repo = repository;
            _logger = logger;
            _auditTrailer = auditTrailer;
        }

        public async Task<QueryResult<IEnumerable<Application>>> Handle(UpdateApplicationCommand request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Entered handler in {@Class} with request {@Request}", nameof(UpdateApplications), request);

             // update audit trail
            var oldObjectResult = await _repo.ReadAsync(request.Application.Name);
            var oldObject = oldObjectResult?.Data?.FirstOrDefault();

            var result =  await _repo.UpdateAsync(request.Application);

            if (result.Result)
            {
                if (request.AuditInformation == null)
                {
                    request.AuditInformation = new AuditInformation("unknown","unknown");
                }
                await _auditTrailer.LogUpdate(request.AuditInformation.Application, request.AuditInformation.Username, oldObject, request.Application, typeof(Application).ToString(), "NA");
                await _auditTrailer.Commit();
            }

            return result;
        }
    }
}

public record DeleteApplications : IRequest<QueryResult<IEnumerable<Application>>>
{
    public sealed record DeleteApplicationCommand : IRequest<QueryResult<IEnumerable<Application>>>
    {
        public string Name { get; set; }
         public AuditInformation AuditInformation { get; set; }
    }

    internal class Handler : IRequestHandler<DeleteApplicationCommand, QueryResult<IEnumerable<Application>>>
    {
        private readonly IApplicationsRepository _repo;
        private readonly ILogger<Handler> _logger;
        private readonly IAuditTrailer _auditTrailer;

        public Handler(IApplicationsRepository repository, ILogger<Handler> logger, IAuditTrailer auditTrailer)
        {
            _repo = repository;
            _logger = logger;
            _auditTrailer = auditTrailer;
        }

        public async Task<QueryResult<IEnumerable<Application>>> Handle(DeleteApplicationCommand request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Entered handler in {@Class} with request {@Request}", nameof(DeleteApplications), request);
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
                await _auditTrailer.LogDelete(request.AuditInformation.Application, request.AuditInformation.Username, oldObject, typeof(Application).ToString(), "NA");
                await _auditTrailer.Commit();
            }

            return result;
        }
    }
}

public record CreateApplications : IRequest<QueryResult<IEnumerable<Application>>>
{
    internal record CreateApplicationCommand : IRequest<QueryResult<IEnumerable<Application>>>
     {
        public Application Application { get; set; }
         public AuditInformation AuditInformation { get; set; }
     }

    internal class Handler : IRequestHandler<CreateApplicationCommand, QueryResult<IEnumerable<Application>>>
    {
        private readonly IApplicationsRepository _repo;
        private readonly ILogger<Handler> _logger;
        private readonly IAuditTrailer _auditTrailer;

        public Handler(IApplicationsRepository repository, ILogger<Handler> logger, IAuditTrailer auditTrailer)
        {
            _repo = repository;
            _logger = logger;
            _auditTrailer = auditTrailer;
        }

        public async Task<QueryResult<IEnumerable<Application>>> Handle(CreateApplicationCommand request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Entered handler in {@Class} with request {@Request}", nameof(CreateApplications), request);

            var result =  await _repo.CreateAsync(request.Application);

            if (result.Result)
            {
                if (request.AuditInformation == null)
                {
                    request.AuditInformation = new AuditInformation("unknown","unknown");
                }
                await _auditTrailer.LogCreate(request.AuditInformation.Application, request.AuditInformation.Username, request.Application, typeof(Application).ToString(), "NA");
                await _auditTrailer.Commit();
            }

            return result;
        }
    }
}
