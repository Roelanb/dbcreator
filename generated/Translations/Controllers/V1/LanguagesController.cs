using DataService.Shared.Entities;
using DataService.Config.Features.Translations.Entities;
using DataService.Config.Features.Translations.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataService.Config.Features.Translations.Controllers.V1;

[ApiController]
[Route("v{version:apiVersion}/[controller]")]
public class LanguagesController : ControllerBase
{
    private readonly IMediator _mediator;

    public LanguagesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Tags("Languages")]
    public async Task<ActionResult<QueryResult<IEnumerable<Language>>>> ReadLanguages()
    {

        var result = await _mediator.Send(new ReadLanguages.Query());

        return Ok(result);
    }

    [HttpGet("{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Tags("Languages")]
    public async Task<ActionResult<QueryResult<IEnumerable<Language>>>> ReadLanguages(string name)
    {

        var result = await _mediator.Send(new ReadLanguages.Query { Name = name});

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Tags("Languages")]
    public async Task<ActionResult<QueryResult<IEnumerable<Language>>>> CreateLanguages(Language language)
    {

        var result = await _mediator.Send(new CreateLanguages.CreateLanguageCommand {Language = language});

        return Ok(result);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Tags("Languages")]
    public async Task<ActionResult<QueryResult<IEnumerable<Language>>>> UpdateLanguages(Language language)
    {

        var result = await _mediator.Send(new UpdateLanguages.UpdateLanguageCommand {Language = language});

        return Ok(result);
    }

    [HttpDelete("{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Tags("Languages")]
    public async Task<ActionResult<QueryResult<IEnumerable<Language>>>> DeleteLanguages(string name)
    {

        var result = await _mediator.Send(new DeleteLanguages.DeleteLanguageCommand {Name = name});

        return Ok(result);
    }
}
