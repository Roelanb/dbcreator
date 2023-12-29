public record ObjectToMap
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
    public string? ExampleObjectListJson{ get; set; }
    
    public string PrimaryKey { get; set; }
    public List<ColumnDefinition> Columns { get; set; }


}
