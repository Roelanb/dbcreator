using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;

public partial class Converter
{
    private string GenerateControllerClassString(string ns, string tableName, string entityName, 
                     List<ColumnDefinition> columns, string primaryKey)
    {
        Console.WriteLine($"Generating Controller class for {tableName}");

        var sb = new StringBuilder();

        sb.AppendLine("using DataService.Shared.Entities;");
        sb.AppendLine("using DataService.Config.Features.Translations.Entities;");
        sb.AppendLine("using DataService.Config.Features.Translations.Queries;");
        sb.AppendLine("using MediatR;");
        sb.AppendLine("using Microsoft.AspNetCore.Http;");
        sb.AppendLine("using Microsoft.AspNetCore.Mvc;");
        
        sb.AppendLine("");
        sb.AppendLine($"namespace {ns}.Controllers.V1;");
        sb.AppendLine("");
        sb.AppendLine($"[ApiController]");
        sb.AppendLine(@"[Route(""v{version:apiVersion}/[controller]"")]");
        sb.AppendLine($"public class {tableName}Controller : ControllerBase");
        sb.AppendLine("{");
        sb.AppendLine($"    private readonly IMediator _mediator;");
        sb.AppendLine("");
        sb.AppendLine($"    public {tableName}Controller(IMediator mediator)");
        sb.AppendLine("    {");
        sb.AppendLine($"        _mediator = mediator;");
        sb.AppendLine("    }");

        sb.AppendLine("");
        sb.AppendLine($"    [HttpGet]");
        sb.AppendLine($"    [ProducesResponseType(StatusCodes.Status200OK)]");
        sb.AppendLine($"    [ProducesResponseType(StatusCodes.Status400BadRequest)]");
        sb.AppendLine($"    [ProducesResponseType(StatusCodes.Status500InternalServerError)]");
        sb.AppendLine($"    [Tags(\"{tableName}\")]");
        sb.AppendLine($"    public async Task<ActionResult<QueryResult<IEnumerable<{entityName}>>>> Read{tableName}()");
        sb.AppendLine("    {");
        sb.AppendLine("");
        sb.AppendLine($"        var result = await _mediator.Send(new Read{tableName}.Query());");
        sb.AppendLine("");
        sb.AppendLine($"        return Ok(result);");
        sb.AppendLine("    }");


        sb.AppendLine("");
        sb.AppendLine($@"    [HttpGet(""{{{ConvertStringToCamelCase(primaryKey)}}}"")]");
        sb.AppendLine($"    [ProducesResponseType(StatusCodes.Status200OK)]");
        sb.AppendLine($"    [ProducesResponseType(StatusCodes.Status400BadRequest)]");
        sb.AppendLine($"    [ProducesResponseType(StatusCodes.Status500InternalServerError)]");
        sb.AppendLine($"    [Tags(\"{tableName}\")]");
        sb.AppendLine($"    public async Task<ActionResult<QueryResult<IEnumerable<{entityName}>>>> Read{tableName}(string {ConvertStringToCamelCase(primaryKey)})");
        sb.AppendLine("    {");
        sb.AppendLine("");
        sb.AppendLine($"        var result = await _mediator.Send(new Read{tableName}.Query {{ {primaryKey} = {ConvertStringToCamelCase(primaryKey)}}});");
        sb.AppendLine("");
        sb.AppendLine($"        return Ok(result);");
        sb.AppendLine("    }");

        sb.AppendLine("");
        sb.AppendLine($"    [HttpPost]");
        sb.AppendLine($"    [ProducesResponseType(StatusCodes.Status200OK)]");
        sb.AppendLine($"    [ProducesResponseType(StatusCodes.Status400BadRequest)]");
        sb.AppendLine($"    [ProducesResponseType(StatusCodes.Status500InternalServerError)]");
        sb.AppendLine($"    [Tags(\"{tableName}\")]");
        sb.AppendLine($"    public async Task<ActionResult<QueryResult<IEnumerable<{entityName}>>>> Create{tableName}({entityName} {entityName.ToLower()})");
        sb.AppendLine("    {");
        sb.AppendLine("");
        sb.AppendLine($"        var result = await _mediator.Send(new Create{tableName}.Create{entityName}Command {{{entityName} = {entityName.ToLower()}}});");
        sb.AppendLine("");
        sb.AppendLine($"        return Ok(result);");
        sb.AppendLine("    }");

        sb.AppendLine("");
        sb.AppendLine($"    [HttpPut]");
        sb.AppendLine($"    [ProducesResponseType(StatusCodes.Status200OK)]");
        sb.AppendLine($"    [ProducesResponseType(StatusCodes.Status400BadRequest)]");
        sb.AppendLine($"    [ProducesResponseType(StatusCodes.Status500InternalServerError)]");
        sb.AppendLine($"    [Tags(\"{tableName}\")]");
        sb.AppendLine($"    public async Task<ActionResult<QueryResult<IEnumerable<{entityName}>>>> Update{tableName}({entityName} {entityName.ToLower()})");
        sb.AppendLine("    {");
        sb.AppendLine("");
        sb.AppendLine($"        var result = await _mediator.Send(new Update{tableName}.Update{entityName}Command {{{entityName} = {entityName.ToLower()}}});");
        sb.AppendLine("");
        sb.AppendLine($"        return Ok(result);");
        sb.AppendLine("    }");


        sb.AppendLine("");
        sb.AppendLine($@"    [HttpDelete(""{{{ConvertStringToCamelCase(primaryKey)}}}"")]");
        sb.AppendLine($"    [ProducesResponseType(StatusCodes.Status200OK)]");
        sb.AppendLine($"    [ProducesResponseType(StatusCodes.Status400BadRequest)]");
        sb.AppendLine($"    [ProducesResponseType(StatusCodes.Status500InternalServerError)]");
        sb.AppendLine($"    [Tags(\"{tableName}\")]");
        sb.AppendLine($"    public async Task<ActionResult<QueryResult<IEnumerable<{entityName}>>>> Delete{tableName}(string {ConvertStringToCamelCase(primaryKey)})");
        sb.AppendLine("    {");
        sb.AppendLine("");
        sb.AppendLine($"        var result = await _mediator.Send(new Delete{tableName}.Delete{entityName}Command {{{primaryKey} = {ConvertStringToCamelCase(primaryKey)}}});");
        sb.AppendLine("");
        sb.AppendLine($"        return Ok(result);");
        sb.AppendLine("    }");


        sb.AppendLine("}");






        return sb.ToString();
    }
}