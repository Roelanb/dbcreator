public class ColumnDefinition
{
    public string ColumnName { get; set; }
    public string DataType { get; set; }
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

    public ColumnDefinition(string columnName, string dataType, string dbType, string sqlType, bool isPrimaryKey, int? maxLength, bool isRequired, string tableHeader, int tableWidth)
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
    
}