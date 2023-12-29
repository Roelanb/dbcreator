using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;

public partial class Converter
{
    public string GenerateControllerClassString(ObjectToMap o)
    {
        Console.WriteLine($"Generating Controller class for {o.TableName}");

        var sb = new StringBuilder();

        sb.AppendLine("using DataService.Shared.Entities;");
        sb.AppendLine($"using DataService.Config.Features.{o.FolderLevel1}.Entities;");
        sb.AppendLine($"using DataService.Config.Features.{o.FolderLevel1}.Queries;");
        sb.AppendLine("using MediatR;");
        sb.AppendLine("using Microsoft.AspNetCore.Http;");
        sb.AppendLine("using Microsoft.AspNetCore.Mvc;");
        
        sb.AppendLine("");
        sb.AppendLine($"namespace {o.NameSpace}.Controllers.V1;");
        sb.AppendLine("");
        sb.AppendLine($"[ApiController]");
        sb.AppendLine(@"[Route(""v{version:apiVersion}/[controller]"")]");
        sb.AppendLine($"public class {o.TableName}Controller : ControllerBase");
        sb.AppendLine("{");
        sb.AppendLine($"    private readonly IMediator _mediator;");
        sb.AppendLine("");
        sb.AppendLine($"    public {o.TableName}Controller(IMediator mediator)");
        sb.AppendLine("    {");
        sb.AppendLine($"        _mediator = mediator;");
        sb.AppendLine("    }");

        sb.AppendLine("");
        sb.AppendLine($"    [HttpGet]");
        sb.AppendLine($"    [ProducesResponseType(StatusCodes.Status200OK)]");
        sb.AppendLine($"    [ProducesResponseType(StatusCodes.Status400BadRequest)]");
        sb.AppendLine($"    [ProducesResponseType(StatusCodes.Status500InternalServerError)]");
        sb.AppendLine($"    [Tags(\"{o.TableName}\")]");
        sb.AppendLine($"    public async Task<ActionResult<QueryResult<IEnumerable<{o.EntityName}>>>> Read{o.TableName}()");
        sb.AppendLine("    {");
        sb.AppendLine("");
        sb.AppendLine($"        var result = await _mediator.Send(new Read{o.TableName}.Query());");
        sb.AppendLine("");
        sb.AppendLine($"        return Ok(result);");
        sb.AppendLine("    }");


        sb.AppendLine("");
        sb.AppendLine($@"    [HttpGet(""{{{ConvertStringToCamelCase(o.PrimaryKey)}}}"")]");
        sb.AppendLine($"    [ProducesResponseType(StatusCodes.Status200OK)]");
        sb.AppendLine($"    [ProducesResponseType(StatusCodes.Status400BadRequest)]");
        sb.AppendLine($"    [ProducesResponseType(StatusCodes.Status500InternalServerError)]");
        sb.AppendLine($"    [Tags(\"{o.TableName}\")]");
        sb.AppendLine($"    public async Task<ActionResult<QueryResult<IEnumerable<{o.EntityName}>>>> Read{o.TableName}(string {ConvertStringToCamelCase(o.PrimaryKey)})");
        sb.AppendLine("    {");
        sb.AppendLine("");
        sb.AppendLine($"        var result = await _mediator.Send(new Read{o.TableName}.Query {{ {o.PrimaryKey} = {ConvertStringToCamelCase(o.PrimaryKey)}}});");
        sb.AppendLine("");
        sb.AppendLine($"        return Ok(result);");
        sb.AppendLine("    }");

        sb.AppendLine("");
        sb.AppendLine($"    [HttpPost]");
        sb.AppendLine($"    [ProducesResponseType(StatusCodes.Status200OK)]");
        sb.AppendLine($"    [ProducesResponseType(StatusCodes.Status400BadRequest)]");
        sb.AppendLine($"    [ProducesResponseType(StatusCodes.Status500InternalServerError)]");
        sb.AppendLine($"    [Tags(\"{o.TableName}\")]");
        sb.AppendLine($"    public async Task<ActionResult<QueryResult<IEnumerable<{o.EntityName}>>>> Create{o.TableName}({o.EntityName} {o.EntityName.ToLower()})");
        sb.AppendLine("    {");
        sb.AppendLine("");
        sb.AppendLine($"        var result = await _mediator.Send(new Create{o.TableName}.Create{o.EntityName}Command {{{o.EntityName} = {o.EntityName.ToLower()}}});");
        sb.AppendLine("");
        sb.AppendLine($"        return Ok(result);");
        sb.AppendLine("    }");

        sb.AppendLine("");
        sb.AppendLine($"    [HttpPut]");
        sb.AppendLine($"    [ProducesResponseType(StatusCodes.Status200OK)]");
        sb.AppendLine($"    [ProducesResponseType(StatusCodes.Status400BadRequest)]");
        sb.AppendLine($"    [ProducesResponseType(StatusCodes.Status500InternalServerError)]");
        sb.AppendLine($"    [Tags(\"{o.TableName}\")]");
        sb.AppendLine($"    public async Task<ActionResult<QueryResult<IEnumerable<{o.EntityName}>>>> Update{o.TableName}({o.EntityName} {o.EntityName.ToLower()})");
        sb.AppendLine("    {");
        sb.AppendLine("");
        sb.AppendLine($"        var result = await _mediator.Send(new Update{o.TableName}.Update{o.EntityName}Command {{{o.EntityName} = {o.EntityName.ToLower()}}});");
        sb.AppendLine("");
        sb.AppendLine($"        return Ok(result);");
        sb.AppendLine("    }");


        sb.AppendLine("");
        sb.AppendLine($@"    [HttpDelete(""{{{ConvertStringToCamelCase(o.PrimaryKey)}}}"")]");
        sb.AppendLine($"    [ProducesResponseType(StatusCodes.Status200OK)]");
        sb.AppendLine($"    [ProducesResponseType(StatusCodes.Status400BadRequest)]");
        sb.AppendLine($"    [ProducesResponseType(StatusCodes.Status500InternalServerError)]");
        sb.AppendLine($"    [Tags(\"{o.TableName}\")]");
        sb.AppendLine($"    public async Task<ActionResult<QueryResult<IEnumerable<{o.EntityName}>>>> Delete{o.TableName}(string {ConvertStringToCamelCase(o.PrimaryKey)})");
        sb.AppendLine("    {");
        sb.AppendLine("");
        sb.AppendLine($"        var result = await _mediator.Send(new Delete{o.TableName}.Delete{o.EntityName}Command {{{o.PrimaryKey} = {ConvertStringToCamelCase(o.PrimaryKey)}}});");
        sb.AppendLine("");
        sb.AppendLine($"        return Ok(result);");
        sb.AppendLine("    }");


        sb.AppendLine("}");






        return sb.ToString();
    }
}