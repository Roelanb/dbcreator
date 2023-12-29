using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;

public partial class Converter
{


    private string GenerateExampleRestCallsStringStructure(ObjectToMap objectToMap)
    {
        Console.WriteLine($"Generating Example Rest Calls for {objectToMap.TableName}");
        
        var sb = new StringBuilder();
        sb.AppendLine("@endpoint=https://localhost:7295/");
        sb.AppendLine("@version=v1");
        sb.AppendLine("");

        sb.AppendLine($"### get {objectToMap.TableName}");
        sb.AppendLine("");
        sb.AppendLine("GET {{endpoint}}{{version}}/"+ objectToMap.TableName);
        sb.AppendLine("Content-Type: application/json");
        sb.AppendLine("");

        sb.AppendLine($"### get {objectToMap.TableName} by KeyField");
        sb.AppendLine("");
        sb.AppendLine("GET {{endpoint}}{{version}}/"+ objectToMap.TableName + "/" + objectToMap.ExampleKeyValue);
        sb.AppendLine("Content-Type: application/json");
        sb.AppendLine("");

        var firstItem = objectToMap.ExampleObjectListJson.Substring(1, objectToMap.ExampleObjectListJson.IndexOf("},", StringComparison.Ordinal) );

        sb.AppendLine($"### post {objectToMap.TableName}");
        sb.AppendLine("");
        sb.AppendLine("POST {{endpoint}}{{version}}/"+ objectToMap.TableName);
        sb.AppendLine("Content-Type: application/json");
        sb.AppendLine("");
        sb.AppendLine($"{firstItem}");

        sb.AppendLine($"### put {objectToMap.TableName}");
        sb.AppendLine("");
        sb.AppendLine("PUT {{endpoint}}{{version}}/" + objectToMap.TableName );
        sb.AppendLine("Content-Type: application/json");
        sb.AppendLine("");
        sb.AppendLine($"{firstItem}");



        sb.AppendLine($"### delete {objectToMap.TableName}");
        sb.AppendLine("");
        sb.AppendLine("DELETE {{endpoint}}{{version}}/"+ objectToMap.TableName + "/" + objectToMap.ExampleKeyValue);
        sb.AppendLine("Content-Type: application/json");
        sb.AppendLine("");


        
        


        return sb.ToString();
    }


}