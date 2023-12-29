using DataService.Shared.Entities;
using DataService.Config.Features.Quality.Entities;
using DataService.Config.Features.Quality.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataService.Config.Features.Quality.Controllers.V1;

[ApiController]
[Route("v{version:apiVersion}/[controller]")]
public class DefectPieceRefsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DefectPieceRefsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Tags("DefectPieceRefs")]
    public async Task<ActionResult<QueryResult<IEnumerable<DefectPieceRefModel>>>> ReadDefectPieceRefs()
    {

        var result = await _mediator.Send(new ReadDefectPieceRefs.Query());

        return Ok(result);
    }

    [HttpGet("{pieceRef}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Tags("DefectPieceRefs")]
    public async Task<ActionResult<QueryResult<IEnumerable<DefectPieceRefModel>>>> ReadDefectPieceRefs(string pieceRef)
    {

        var result = await _mediator.Send(new ReadDefectPieceRefs.Query { PieceRef = pieceRef});

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Tags("DefectPieceRefs")]
    public async Task<ActionResult<QueryResult<IEnumerable<DefectPieceRefModel>>>> CreateDefectPieceRefs(DefectPieceRefModel defectpiecerefmodel)
    {

        var result = await _mediator.Send(new CreateDefectPieceRefs.CreateDefectPieceRefModelCommand {DefectPieceRefModel = defectpiecerefmodel});

        return Ok(result);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Tags("DefectPieceRefs")]
    public async Task<ActionResult<QueryResult<IEnumerable<DefectPieceRefModel>>>> UpdateDefectPieceRefs(DefectPieceRefModel defectpiecerefmodel)
    {

        var result = await _mediator.Send(new UpdateDefectPieceRefs.UpdateDefectPieceRefModelCommand {DefectPieceRefModel = defectpiecerefmodel});

        return Ok(result);
    }

    [HttpDelete("{pieceRef}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Tags("DefectPieceRefs")]
    public async Task<ActionResult<QueryResult<IEnumerable<DefectPieceRefModel>>>> DeleteDefectPieceRefs(string pieceRef)
    {

        var result = await _mediator.Send(new DeleteDefectPieceRefs.DeleteDefectPieceRefModelCommand {PieceRef = pieceRef});

        return Ok(result);
    }
}
