using DataService.Shared.Entities;
using DataService.Config.Features.Translations.Entities;
using DataService.Config.Features.Translations.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataService.Config.Features.Translations.Controllers.V1;

[ApiController]
[Route("v{version:apiVersion}/[controller]")]
public class TranslationsTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public TranslationsTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Tags("TranslationsTypes")]
    public async Task<ActionResult<QueryResult<IEnumerable<TranslationsType>>>> ReadTranslationsTypes()
    {

        var result = await _mediator.Send(new ReadTranslationsTypes.Query());

        return Ok(result);
    }

    [HttpGet("{translation_type}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Tags("TranslationsTypes")]
    public async Task<ActionResult<QueryResult<IEnumerable<TranslationsType>>>> ReadTranslationsTypes(string translation_type)
    {

        var result = await _mediator.Send(new ReadTranslationsTypes.Query { translation_type = translation_type});

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Tags("TranslationsTypes")]
    public async Task<ActionResult<QueryResult<IEnumerable<TranslationsType>>>> CreateTranslationsTypes(TranslationsType translationstype)
    {

        var result = await _mediator.Send(new CreateTranslationsTypes.CreateTranslationsTypeCommand {TranslationsType = translationstype});

        return Ok(result);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Tags("TranslationsTypes")]
    public async Task<ActionResult<QueryResult<IEnumerable<TranslationsType>>>> UpdateTranslationsTypes(TranslationsType translationstype)
    {

        var result = await _mediator.Send(new UpdateTranslationsTypes.UpdateTranslationsTypeCommand {TranslationsType = translationstype});

        return Ok(result);
    }

    [HttpDelete("{translation_type}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Tags("TranslationsTypes")]
    public async Task<ActionResult<QueryResult<IEnumerable<TranslationsType>>>> DeleteTranslationsTypes(string translation_type)
    {

        var result = await _mediator.Send(new DeleteTranslationsTypes.DeleteTranslationsTypeCommand {translation_type = translation_type});

        return Ok(result);
    }
}
