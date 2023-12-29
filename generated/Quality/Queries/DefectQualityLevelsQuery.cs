using DataService.Shared.Entities;
using DataService.Config.Features.Quality.Entities;
using DataService.Config.Features.Quality.Shared;
using AuditTrail.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using DataService.Shared.Extensions;

namespace DataService.Config.Features.Quality.Queries;

public record ReadDefectQualityLevels : IRequest<QueryResult<IEnumerable<DefectQualityLevelModel>>>
{
    public sealed record Query() : IRequest<QueryResult<IEnumerable<DefectQualityLevelModel>>>
    {
        public string? DefectQualityLevel { get; set; } = null;
    }

    internal class Handler : IRequestHandler<Query, QueryResult<IEnumerable<DefectQualityLevelModel>>>
    {
        private readonly IDefectQualityLevelsRepository _repo;
        private readonly ILogger<Handler> _logger;

        public Handler(IDefectQualityLevelsRepository repository, ILogger<Handler> logger)
        {
            _repo = repository;
            _logger = logger;
        }

        public async Task<QueryResult<IEnumerable<DefectQualityLevelModel>>> Handle(Query request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Entered handler in {@Class} with request {@Request}", nameof(ReadDefectQualityLevels), request);

            return await _repo.ReadAsync(request.DefectQualityLevel);
        }
    }
}

public record UpdateDefectQualityLevels : IRequest<QueryResult<IEnumerable<DefectQualityLevelModel>>>
{
    public sealed record UpdateDefectQualityLevelModelCommand : IRequest<QueryResult<IEnumerable<DefectQualityLevelModel>>>
    {
        public DefectQualityLevelModel DefectQualityLevelModel { get; set; }
         public AuditInformation AuditInformation { get; set; }
    }

    internal class Handler : IRequestHandler<UpdateDefectQualityLevelModelCommand, QueryResult<IEnumerable<DefectQualityLevelModel>>>
    {
        private readonly IDefectQualityLevelsRepository _repo;
        private readonly ILogger<Handler> _logger;
        private readonly IAuditTrailer _auditTrailer;

        public Handler(IDefectQualityLevelsRepository repository, ILogger<Handler> logger, IAuditTrailer auditTrailer)
        {
            _repo = repository;
            _logger = logger;
            _auditTrailer = auditTrailer;
        }

        public async Task<QueryResult<IEnumerable<DefectQualityLevelModel>>> Handle(UpdateDefectQualityLevelModelCommand request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Entered handler in {@Class} with request {@Request}", nameof(UpdateDefectQualityLevels), request);

             // update audit trail
            var oldObjectResult = await _repo.ReadAsync(request.DefectQualityLevelModel.DefectQualityLevel);
            var oldObject = oldObjectResult?.Data?.FirstOrDefault();

            var result =  await _repo.UpdateAsync(request.DefectQualityLevelModel);

            if (result.Result)
            {
                if (request.AuditInformation == null)
                {
                    request.AuditInformation = new AuditInformation("unknown","unknown");
                }
                await _auditTrailer.LogUpdate(request.AuditInformation.Application, request.AuditInformation.Username, oldObject, request.DefectQualityLevelModel, typeof(DefectQualityLevelModel).ToString(), "NA");
                await _auditTrailer.Commit();
            }

            return result;
        }
    }
}

public record DeleteDefectQualityLevels : IRequest<QueryResult<IEnumerable<DefectQualityLevelModel>>>
{
    public sealed record DeleteDefectQualityLevelModelCommand : IRequest<QueryResult<IEnumerable<DefectQualityLevelModel>>>
    {
        public string DefectQualityLevel { get; set; }
         public AuditInformation AuditInformation { get; set; }
    }

    internal class Handler : IRequestHandler<DeleteDefectQualityLevelModelCommand, QueryResult<IEnumerable<DefectQualityLevelModel>>>
    {
        private readonly IDefectQualityLevelsRepository _repo;
        private readonly ILogger<Handler> _logger;
        private readonly IAuditTrailer _auditTrailer;

        public Handler(IDefectQualityLevelsRepository repository, ILogger<Handler> logger, IAuditTrailer auditTrailer)
        {
            _repo = repository;
            _logger = logger;
            _auditTrailer = auditTrailer;
        }

        public async Task<QueryResult<IEnumerable<DefectQualityLevelModel>>> Handle(DeleteDefectQualityLevelModelCommand request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Entered handler in {@Class} with request {@Request}", nameof(DeleteDefectQualityLevels), request);
             // update audit trail
            var oldObjectResult = await _repo.ReadAsync(request.DefectQualityLevel);
            var oldObject = oldObjectResult?.Data?.FirstOrDefault();

            var result =  await _repo.DeleteAsync(request.DefectQualityLevel);

            if (result.Result)
            {
                if (request.AuditInformation == null)
                {
                    request.AuditInformation = new AuditInformation("unknown","unknown");
                }
                await _auditTrailer.LogDelete(request.AuditInformation.Application, request.AuditInformation.Username, oldObject, typeof(DefectQualityLevelModel).ToString(), "NA");
                await _auditTrailer.Commit();
            }

            return result;
        }
    }
}

public record CreateDefectQualityLevels : IRequest<QueryResult<IEnumerable<DefectQualityLevelModel>>>
{
    internal record CreateDefectQualityLevelModelCommand : IRequest<QueryResult<IEnumerable<DefectQualityLevelModel>>>
     {
        public DefectQualityLevelModel DefectQualityLevelModel { get; set; }
         public AuditInformation AuditInformation { get; set; }
     }

    internal class Handler : IRequestHandler<CreateDefectQualityLevelModelCommand, QueryResult<IEnumerable<DefectQualityLevelModel>>>
    {
        private readonly IDefectQualityLevelsRepository _repo;
        private readonly ILogger<Handler> _logger;
        private readonly IAuditTrailer _auditTrailer;

        public Handler(IDefectQualityLevelsRepository repository, ILogger<Handler> logger, IAuditTrailer auditTrailer)
        {
            _repo = repository;
            _logger = logger;
            _auditTrailer = auditTrailer;
        }

        public async Task<QueryResult<IEnumerable<DefectQualityLevelModel>>> Handle(CreateDefectQualityLevelModelCommand request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Entered handler in {@Class} with request {@Request}", nameof(CreateDefectQualityLevels), request);

            var result =  await _repo.CreateAsync(request.DefectQualityLevelModel);

            if (result.Result)
            {
                if (request.AuditInformation == null)
                {
                    request.AuditInformation = new AuditInformation("unknown","unknown");
                }
                await _auditTrailer.LogCreate(request.AuditInformation.Application, request.AuditInformation.Username, request.DefectQualityLevelModel, typeof(DefectQualityLevelModel).ToString(), "NA");
                await _auditTrailer.Commit();
            }

            return result;
        }
    }
}
