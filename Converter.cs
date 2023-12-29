using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;

public partial class Converter
{
    public string Version { get; set; } = "CrudCreator v1.0.0";
    
    public string GenerateEntityCSharpClass(ObjectToMap objectToMap)
    {
        return GenerateEntityClassString(objectToMap, objectToMap.Columns);
    }

    public string GenerateRepositoryClassString(ObjectToMap objectToMap)
    {
        
        return GenerateRepositoryClassString(objectToMap.NameSpace, objectToMap.TableName, objectToMap.EntityName, objectToMap.Columns, objectToMap.PrimaryKey);
    }

    public string GenerateIRepositoryClassString(ObjectToMap objectToMap)
    {
        return GenerateIRepositoryClassString(objectToMap.NameSpace, objectToMap.TableName, objectToMap.EntityName, objectToMap.Columns, objectToMap.PrimaryKey);
    }

    public string GenerateQueryClassString(ObjectToMap objectToMap)
    {
        return GenerateQueryClassString(objectToMap.NameSpace, objectToMap.TableName, objectToMap.EntityName, objectToMap.Columns, objectToMap.PrimaryKey);
    }


    public string GenerateControllerClassString(ObjectToMap objectToMap)
    {
        return GenerateControllerClassString(objectToMap.NameSpace, objectToMap.TableName, objectToMap.EntityName, objectToMap.Columns, objectToMap.PrimaryKey);
    }

    public string GenerateExampleRestCallsString(ObjectToMap objectToMap)
    {
       
        return GenerateExampleRestCallsStringStructure(objectToMap);
    }
    public void GenerateDatabaseObjects(string databaseFolder, ObjectToMap objectToMap)
    {
        
        GenerateDatabaseObjectsFiles(databaseFolder,objectToMap);
    }

    private string SqlTypeToDbType(string sqlType)
    {
        switch (sqlType.ToLower())
        {
            case "int":
                return "Int32";
            case "smallint":
                return "Int16";
            case "bigint":
                return "Int64";
            case "bit":
                return "Boolean";
            case "varchar":
            case "nvarchar":
            case "text":
                return "String";
            case "datetime":
                return "DateTime";
            case "float":
                return "Double";
            case "decimal":
                return "Decimal";
            case "binary":
            case "varbinary":
            case "image":
                return "Byte[]";
            default:
                return "Object";
        }
    }

    private string SqlTypeToCSharpType(string sqlType)
    {
        switch (sqlType.ToLower())
        {
            case "int":
                return "int";
            case "smallint":
                return "short";
            case "bigint":
                return "long";
            case "bit":
                return "bool";
            case "varchar":
            case "nvarchar":
            case "text":
                return "string";
            case "datetime":
                return "DateTime";
            case "float":
                return "double";
            case "decimal":
                return "decimal";
            case "binary":
            case "varbinary":
            case "image":
                return "byte[]";
            default:
                return "object";
        }
    }

    private string ConvertStringToCamelCase(string input)
    {
        return input.Substring(0, 1).ToLower() + input.Substring(1);
    }


}