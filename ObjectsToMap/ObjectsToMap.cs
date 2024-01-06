using System.Text;

public class ObjectToMap
{
    public string? Name { get; set; }
    public string? TableName { get; set; }
    public string? EntityName { get; set; }

    public string FolderLevel1 { get; set; }

    public string UiFolderLevel1 { get; set; }

    public string? ConnectionString { get; set; }

    public string? NameSpace { get; set; }

    public string? UiNameSpace { get; set; }

    public string? ExampleKeyValue { get; set; }
    public string? ExampleObjectListJson { get; set; }

    public string? PrimaryKey { get; set; }
    public List<ColumnDefinition> Columns { get; set; }


    public string? SourceSqlTableCreateCommand { get; set; }


    public void LoadColumnsFromSqlCreateCommand()
    {
        if (string.IsNullOrEmpty(SourceSqlTableCreateCommand))
        {
            return;
        }

        var lines = SourceSqlTableCreateCommand.Split("\r\n");
        var columns = new List<ColumnDefinition>();
        foreach (var line in lines.Select(l => l.Trim()))
        {
            if (line.StartsWith("CREATE TABLE"))
            {
                continue;
            }

            if (line.StartsWith("(") || line.StartsWith(")"))
            {
                continue;
            }

            if (line.StartsWith("CONSTRAINT") || line.StartsWith("PRIMARY KEY") || line.StartsWith("FOREIGN KEY") || line.StartsWith("UNIQUE") || line.StartsWith("CHECK"))
            {
                continue;
            }


            if (line.StartsWith("GO") || line.StartsWith("SET") || line.StartsWith("PRINT") || line.StartsWith("EXEC"))
            {
                continue;
            }

            if (line.StartsWith("--") || line.StartsWith("/*") || line.StartsWith("*/"))
            {
                continue;
            }



            if (line.StartsWith("ALTER") || line.StartsWith("DROP") || line.StartsWith("IF") || line.StartsWith("INSERT") || line.StartsWith("UPDATE") || line.StartsWith("DELETE") || line.StartsWith("BEGIN") || line.StartsWith("END"))
            {
                continue;
            }


            if (line.StartsWith("[") && !line.Contains("ASC") && !line.Contains("DESC"))
            {
                var columnLine = line;
                var isNullable = !columnLine.Contains("NOT NULL");

                var columnName = columnLine.Substring(1, columnLine.IndexOf("]") - 1);
                columnLine = columnLine.Substring(columnLine.IndexOf("]") + 1).Trim();

                var dataType = columnLine.Substring(0, columnLine.IndexOf(" ")).Trim().Replace("[", "").Replace("]", "");
                columnLine = columnLine.Substring(columnLine.IndexOf(" ") + 1).Trim();

                var columnDataType = ColumnDataType.String;
                var dbType = "String";
                var sqlType = "nvarchar";
                int? maxLength = null;
                var columnWitdh = 250;

                if (dataType == "int")
                {
                    columnDataType = ColumnDataType.Int;
                    dbType = "Int32";
                    sqlType = "int";
                    columnWitdh = 100;
                }
                if (dataType == "bit")
                {
                    columnDataType = ColumnDataType.Bool;
                    dbType = "Boolean";
                    sqlType = "bit";
                    columnWitdh = 100;
                }
                if (dataType == "datetime")
                {
                    columnDataType = ColumnDataType.DateTime;
                    dbType = "DateTime";
                    sqlType = "datetime";
                    columnWitdh = 150;
                }
                if (dataType == "decimal")
                {
                    columnDataType = ColumnDataType.Decimal;
                    dbType = "Decimal";
                    sqlType = "decimal";
                    columnWitdh = 100;
                }
                if (dataType == "float")
                {
                    columnDataType = ColumnDataType.Float;
                    dbType = "Float";
                    sqlType = "float";
                    columnWitdh = 100;
                }
                if (dataType.Contains("nvarchar"))
                {
                    columnDataType = ColumnDataType.String;
                    maxLength = int.Parse(dataType.Substring(dataType.IndexOf("(") + 1, dataType.IndexOf(")") - dataType.IndexOf("(") - 1));
                    dbType = "String";
                    sqlType = "nvarchar";
                    columnWitdh = 250;
                }

                Console.WriteLine($"Column: {columnName} {dataType} {isNullable}");



                columns.Add(new ColumnDefinition(columnName, columnDataType, dbType, sqlType, isNullable, maxLength, true, columnName, columnWitdh));
            }
            if (line.Contains("ASC") || line.Contains("DESC"))
            {
                var keyName = line.Substring(line.IndexOf("[") + 1, line.IndexOf("]") - line.IndexOf("[") - 1);
                Console.WriteLine($"Key: {keyName}");

                PrimaryKey = keyName;
                var keyColumn = columns.FirstOrDefault(c => c.ColumnName == keyName);
                if (keyColumn != null)
                {
                    keyColumn.IsPrimaryKey = true;
                    keyColumn.IsNullable = false;
                }
            }
        }

        Columns = new List<ColumnDefinition>();

        foreach (var column in columns)
        {
            Console.WriteLine($"Column: {column.ColumnName}  {column.DataType} {column.SqlType} {column.DbType} {column.IsNullable} Maxlength: {column.MaxLength}");


            if (Columns.All(c => c.ColumnName != column.ColumnName))
            {
                Columns.Add(column);
            }
        }

        RecalculateColumnWidths();

        var columnString = GenerateColumnDefinitions();

        var objectDefinition = File.ReadAllText(@$"D:\Projects\crud\dbcreator\ObjectsToMap\otm_{Name}.cs");

        var replaceLocationStart = objectDefinition.IndexOf("// *** COLUMNS START ***") + "// *** COLUMNS START ***".Length;
        var replaceLocationEnd = objectDefinition.IndexOf("// *** COLUMNS END ***");

        objectDefinition = objectDefinition.Remove(replaceLocationStart, replaceLocationEnd - replaceLocationStart);


        objectDefinition = objectDefinition.Replace("// *** COLUMNS START ***", "// *** COLUMNS START ***\r\n" + columnString);


        File.WriteAllText(@$"D:\Projects\crud\dbcreator\ObjectsToMap\otm_{Name}.cs", objectDefinition);

    }

    public string GenerateColumnDefinitions()
    {
        var sb = new StringBuilder();

        sb.AppendLine($"PrimaryKey = \"{PrimaryKey}\",");
        sb.AppendLine($"Columns = new List<ColumnDefinition>");
        sb.AppendLine($"{{");

        var columns = new List<ColumnDefinition>();
        foreach (var column in Columns)
        {
            sb.AppendLine($"new ColumnDefinition(\"{column.ColumnName}\", ColumnDataType.{column.DataType}, \"{column.DbType}\",\"{column.SqlType}\", {column.IsPrimaryKey.ToString().ToLower()}, {column.MaxLength?.ToString() ?? "null"}, {column.IsRequired.ToString().ToLower()}, \"{column.TableHeader}\", {column.TableWidth}),");
        }

        sb.AppendLine($"}},");

        Console.WriteLine(sb.ToString());

        return sb.ToString();
    }

    public void RecalculateColumnWidths()
    {
        var columnWidths = new List<int>();
        foreach (var column in Columns)
        {
            if (column.DataType == ColumnDataType.Bool)
            {
                column.TableWidth = 100;
            }
            if (column.DataType == ColumnDataType.DateTime)
            {
                column.TableWidth = 150;
            }
            if (column.DataType == ColumnDataType.Decimal)
            {
                column.TableWidth = 100;
            }
            if (column.DataType == ColumnDataType.Float)
            {
                column.TableWidth = 100;
            }
            if (column.DataType == ColumnDataType.Int)
            {
                column.TableWidth = 100;
            }
            if (column.DataType == ColumnDataType.String)
            {
                column.TableWidth = 250;
            }

        }

    }
}
