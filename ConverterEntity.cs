using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;

public partial class Converter
{


    private string GenerateEntityClassString(ObjectToMap o, List<ColumnDefinition> columns)
    {
        Console.WriteLine($"Generating entity class for {o.TableName}");

        var sb = new StringBuilder();
        sb.AppendLine("using System.ComponentModel.DataAnnotations.Schema;");
        sb.AppendLine("using System.ComponentModel.DataAnnotations;");
        sb.AppendLine("");
        sb.AppendLine($"namespace {o.NameSpace}.Entities;");
        sb.AppendLine("");
        sb.AppendLine(@$"[Table(""{o.TableName}"", Schema = ""dbo"")]");
        sb.AppendLine($"public class {o.EntityName}");
        sb.AppendLine("{");
        foreach (var column in columns)
        {
            if (column.IsPrimaryKey)
            {
                sb.AppendLine($"    [Key]");
            }
            if (column.IsRequired)
            {
                sb.AppendLine($"    [Required]");
            }
            if (column.MaxLength.HasValue && column.DataType == "string")
            {
                sb.AppendLine($"    [StringLength({column.MaxLength.Value})]");
            }
            sb.AppendLine($"    public {column.DataType} {column.ColumnName} {{ get; set; }}");
        }
        sb.AppendLine("}");
        return sb.ToString();
    }


}