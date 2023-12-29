using DataService.Shared.Entities;
using DataService.Config.Features.Quality.Entities;
using DataService.Config.Features.Quality.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataService.Config.Features.Quality.Controllers.V1;

[ApiController]
[Route("v{version:apiVersion}/[controller]")]
public class DefectQualityLevelsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DefectQualityLevelsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Tags("DefectQualityLevels")]
    public async Task<ActionResult<QueryResult<IEnumerable<DefectQualityLevelModel>>>> ReadDefectQualityLevels()
    {

        var result = await _mediator.Send(new ReadDefectQualityLevels.Query());

        return Ok(result);
    }

    [HttpGet("{defectQualityLevel}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Tags("DefectQualityLevels")]
    public async Task<ActionResult<QueryResult<IEnumerable<DefectQualityLevelModel>>>> ReadDefectQualityLevels(string defectQualityLevel)
    {

        var result = await _mediator.Send(new ReadDefectQualityLevels.Query { DefectQualityLevel = defectQualityLevel});

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Tags("DefectQualityLevels")]
    public async Task<ActionResult<QueryResult<IEnumerable<DefectQualityLevelModel>>>> CreateDefectQualityLevels(DefectQualityLevelModel defectqualitylevelmodel)
    {

        var result = await _mediator.Send(new CreateDefectQualityLevels.CreateDefectQualityLevelModelCommand {DefectQualityLevelModel = defectqualitylevelmodel});

        return Ok(result);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Tags("DefectQualityLevels")]
    public async Task<ActionResult<QueryResult<IEnumerable<DefectQualityLevelModel>>>> UpdateDefectQualityLevels(DefectQualityLevelModel defectqualitylevelmodel)
    {

        var result = await _mediator.Send(new UpdateDefectQualityLevels.UpdateDefectQualityLevelModelCommand {DefectQualityLevelModel = defectqualitylevelmodel});

        return Ok(result);
    }

    [HttpDelete("{defectQualityLevel}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Tags("DefectQualityLevels")]
    public async Task<ActionResult<QueryResult<IEnumerable<DefectQualityLevelModel>>>> DeleteDefectQualityLevels(string defectQualityLevel)
    {

        var result = await _mediator.Send(new DeleteDefectQualityLevels.DeleteDefectQualityLevelModelCommand {DefectQualityLevel = defectQualityLevel});

        return Ok(result);
    }
}
