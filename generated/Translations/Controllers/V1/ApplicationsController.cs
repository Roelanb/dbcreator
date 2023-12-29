using DataService.Shared.Entities;
using DataService.Config.Features.Translations.Entities;
using DataService.Config.Features.Translations.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataService.Config.Features.Translations.Controllers.V1;

[ApiController]
[Route("v{version:apiVersion}/[controller]")]
public class ApplicationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ApplicationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Tags("Applications")]
    public async Task<ActionResult<QueryResult<IEnumerable<Application>>>> ReadApplications()
    {

        var result = await _mediator.Send(new ReadApplications.Query());

        return Ok(result);
    }

    [HttpGet("{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Tags("Applications")]
    public async Task<ActionResult<QueryResult<IEnumerable<Application>>>> ReadApplications(string name)
    {

        var result = await _mediator.Send(new ReadApplications.Query { Name = name});

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Tags("Applications")]
    public async Task<ActionResult<QueryResult<IEnumerable<Application>>>> CreateApplications(Application application)
    {

        var result = await _mediator.Send(new CreateApplications.CreateApplicationCommand {Application = application});

        return Ok(result);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Tags("Applications")]
    public async Task<ActionResult<QueryResult<IEnumerable<Application>>>> UpdateApplications(Application application)
    {

        var result = await _mediator.Send(new UpdateApplications.UpdateApplicationCommand {Application = application});

        return Ok(result);
    }

    [HttpDelete("{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Tags("Applications")]
    public async Task<ActionResult<QueryResult<IEnumerable<Application>>>> DeleteApplications(string name)
    {

        var result = await _mediator.Send(new DeleteApplications.DeleteApplicationCommand {Name = name});

        return Ok(result);
    }
}
