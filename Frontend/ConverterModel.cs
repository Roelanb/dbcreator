using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;


public partial class Converter
{


    public string GenerateModelClassString(ObjectToMap o)
    {
        var columns = o.Columns;

        Console.WriteLine($"Generating model class for {o.TableName}");

        var sb = new StringBuilder();
        sb.AppendLine($"// Auto generated with {Version}");
        sb.AppendLine("using System.ComponentModel.DataAnnotations.Schema;");
        sb.AppendLine("using System.ComponentModel.DataAnnotations;");
        sb.AppendLine("");
        sb.AppendLine($"namespace {o.UiNameSpace}.Features.Config.Models;");
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
                sb.AppendLine($"    [Required(ErrorMessage = \"{column.ColumnName} is required\")]");
                sb.AppendLine($"    [DisplayFormat(NullDisplayText = \"Empty\", ConvertEmptyStringToNull = true)]");
            }
            if (column.MaxLength.HasValue && column.DataType == ColumnDataType.String)
            {
                sb.AppendLine($"    [StringLength({column.MaxLength.Value})]");
            }
            
            sb.AppendLine($"    public {column.DataTypeToCSharpType()} {column.ColumnName} {{ get; set; }}");
        }
        sb.AppendLine("}");
        return sb.ToString();
    }


}