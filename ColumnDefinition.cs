public enum ColumnDataType
{
    String,
    Int,
    Bool,
    DateTime,
    Float,
    Decimal,
    Guid
}

public class ColumnDefinition
{
    public string ColumnName { get; set; }
    public ColumnDataType DataType { get; set; }
    public string DbType { get; set; }
    public string SqlType { get; set; }
    public bool IsPrimaryKey { get; set; }
    public int? MaxLength { get; set; }
    public bool IsRequired { get; set; }
    public bool IsNullable { get; set; }
    
    // grid data annotation
    public float? MinValue { get; set; }
    public float? MaxValue { get; set; }
    public string? DataFormat { get; set; }

    public string TableHeader { get; set; }
    public int TableWidth { get; set; }

    public ColumnDefinition(string columnName, ColumnDataType dataType, string dbType, string sqlType, bool isPrimaryKey, int? maxLength, bool isRequired, string tableHeader, int tableWidth)
    {
        ColumnName = columnName;
        DataType = dataType;
        DbType = dbType;
        SqlType = sqlType;
        IsPrimaryKey = isPrimaryKey;
        MaxLength = maxLength;
        IsRequired = isRequired;
        IsNullable = !isRequired;
        TableHeader = tableHeader;
        TableWidth = tableWidth;
    }

    public string DataTypeToCSharpType()
    {
        switch (DataType)
        {
            case ColumnDataType.String:
                return "string";
            case ColumnDataType.Int:
                return "int";
            case ColumnDataType.Bool:
                return "bool";
            case ColumnDataType.DateTime:
                return "DateTime";
            case ColumnDataType.Float:
                return "float";
            case ColumnDataType.Decimal:
                return "decimal";
            case ColumnDataType.Guid:
                return "Guid";
            default:
                throw new ArgumentOutOfRangeException();
        }
    }   
}