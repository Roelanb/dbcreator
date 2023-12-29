using DataService.Shared.Entities;
using DataService.Config.Features.Quality.Entities;
using DataService.Config.Features.Quality.Shared;
using AuditTrail.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using DataService.Shared.Extensions;

namespace DataService.Config.Features.Quality.Queries;

public record ReadDefectPieceRefs : IRequest<QueryResult<IEnumerable<DefectPieceRefModel>>>
{
    public sealed record Query() : IRequest<QueryResult<IEnumerable<DefectPieceRefModel>>>
    {
        public string? PieceRef { get; set; } = null;
    }

    internal class Handler : IRequestHandler<Query, QueryResult<IEnumerable<DefectPieceRefModel>>>
    {
        private readonly IDefectPieceRefsRepository _repo;
        private readonly ILogger<Handler> _logger;

        public Handler(IDefectPieceRefsRepository repository, ILogger<Handler> logger)
        {
            _repo = repository;
            _logger = logger;
        }

        public async Task<QueryResult<IEnumerable<DefectPieceRefModel>>> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Entered handler in {@Class} with request {@Request}", nameof(ReadDefectPieceRefs), request);

            return await _repo.ReadAsync(request.PieceRef);
        }
    }
}

public record UpdateDefectPieceRefs : IRequest<QueryResult<IEnumerable<DefectPieceRefModel>>>
{
    public sealed record UpdateDefectPieceRefModelCommand : IRequest<QueryResult<IEnumerable<DefectPieceRefModel>>>
    {
        public DefectPieceRefModel DefectPieceRefModel { get; set; }
         public AuditInformation AuditInformation { get; set; }
    }

    internal class Handler : IRequestHandler<UpdateDefectPieceRefModelCommand, QueryResult<IEnumerable<DefectPieceRefModel>>>
    {
        private readonly IDefectPieceRefsRepository _repo;
        private readonly ILogger<Handler> _logger;
        private readonly IAuditTrailer _auditTrailer;

        public Handler(IDefectPieceRefsRepository repository, ILogger<Handler> logger, IAuditTrailer auditTrailer)
        {
            _repo = repository;
            _logger = logger;
            _auditTrailer = auditTrailer;
        }

        public async Task<QueryResult<IEnumerable<DefectPieceRefModel>>> Handle(UpdateDefectPieceRefModelCommand request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Entered handler in {@Class} with request {@Request}", nameof(UpdateDefectPieceRefs), request);

             // update audit trail
            var oldObjectResult = await _repo.ReadAsync(request.DefectPieceRefModel.PieceRef);
            var oldObject = oldObjectResult?.Data?.FirstOrDefault();

            var result =  await _repo.UpdateAsync(request.DefectPieceRefModel);

            if (result.Result)
            {
                if (request.AuditInformation == null)
                {
                    request.AuditInformation = new AuditInformation("unknown","unknown");
                }
                await _auditTrailer.LogUpdate(request.AuditInformation.Application, request.AuditInformation.Username, oldObject, request.DefectPieceRefModel, typeof(DefectPieceRefModel).ToString(), "NA");
                await _auditTrailer.Commit();
            }

            return result;
        }
    }
}

public record DeleteDefectPieceRefs : IRequest<QueryResult<IEnumerable<DefectPieceRefModel>>>
{
    public sealed record DeleteDefectPieceRefModelCommand : IRequest<QueryResult<IEnumerable<DefectPieceRefModel>>>
    {
        public string PieceRef { get; set; }
         public AuditInformation AuditInformation { get; set; }
    }

    internal class Handler : IRequestHandler<DeleteDefectPieceRefModelCommand, QueryResult<IEnumerable<DefectPieceRefModel>>>
    {
        private readonly IDefectPieceRefsRepository _repo;
        private readonly ILogger<Handler> _logger;
        private readonly IAuditTrailer _auditTrailer;

        public Handler(IDefectPieceRefsRepository repository, ILogger<Handler> logger, IAuditTrailer auditTrailer)
        {
            _repo = repository;
            _logger = logger;
            _auditTrailer = auditTrailer;
        }

        public async Task<QueryResult<IEnumerable<DefectPieceRefModel>>> Handle(DeleteDefectPieceRefModelCommand request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Entered handler in {@Class} with request {@Request}", nameof(DeleteDefectPieceRefs), request);
             // update audit trail
            var oldObjectResult = await _repo.ReadAsync(request.PieceRef);
            var oldObject = oldObjectResult?.Data?.FirstOrDefault();

            var result =  await _repo.DeleteAsync(request.PieceRef);

            if (result.Result)
            {
                if (request.AuditInformation == null)
                {
                    request.AuditInformation = new AuditInformation("unknown","unknown");
                }
                await _auditTrailer.LogDelete(request.AuditInformation.Application, request.AuditInformation.Username, oldObject, typeof(DefectPieceRefModel).ToString(), "NA");
                await _auditTrailer.Commit();
            }

            return result;
        }
    }
}

public record CreateDefectPieceRefs : IRequest<QueryResult<IEnumerable<DefectPieceRefModel>>>
{
    internal record CreateDefectPieceRefModelCommand : IRequest<QueryResult<IEnumerable<DefectPieceRefModel>>>
     {
        public DefectPieceRefModel DefectPieceRefModel { get; set; }
         public AuditInformation AuditInformation { get; set; }
     }

    internal class Handler : IRequestHandler<CreateDefectPieceRefModelCommand, QueryResult<IEnumerable<DefectPieceRefModel>>>
    {
        private readonly IDefectPieceRefsRepository _repo;
        private readonly ILogger<Handler> _logger;
        private readonly IAuditTrailer _auditTrailer;

        public Handler(IDefectPieceRefsRepository repository, ILogger<Handler> logger, IAuditTrailer auditTrailer)
        {
            _repo = repository;
            _logger = logger;
            _auditTrailer = auditTrailer;
        }

        public async Task<QueryResult<IEnumerable<DefectPieceRefModel>>> Handle(CreateDefectPieceRefModelCommand request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Entered handler in {@Class} with request {@Request}", nameof(CreateDefectPieceRefs), request);

            var result =  await _repo.CreateAsync(request.DefectPieceRefModel);

            if (result.Result)
            {
                if (request.AuditInformation == null)
                {
                    request.AuditInformation = new AuditInformation("unknown","unknown");
                }
                await _auditTrailer.LogCreate(request.AuditInformation.Application, request.AuditInformation.Username, request.DefectPieceRefModel, typeof(DefectPieceRefModel).ToString(), "NA");
                await _auditTrailer.Commit();
            }

            return result;
        }
    }
}
