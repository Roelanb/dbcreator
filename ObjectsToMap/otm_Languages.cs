public static partial class ObjectsToMapData
{
    public static ObjectToMap Languages(string connectionString)
    {
        var objectToMap = new ObjectToMap
        {
            Name = "Languages",
            TableName = "Languages",
            EntityName = "Language",
            FolderLevel1 = "Translations",
            UiFolderLevel1 = "Config",
            ConnectionString = connectionString,
            NameSpace = "DataService.Config.Features.Translations",
            UiNameSpace = "MesExplorer",

            ExampleKeyValue = "ZULU",
            ExampleObjectListJson = @"[
                {
                    ""name"": ""ZULU"",
                    ""isActive"": true
                },
                {
                    ""name"": ""ENGLISH"",
                    ""isActive"": true
                }
                ]",

            PrimaryKey = "Name",
            Columns = new List<ColumnDefinition>
    {
        new ColumnDefinition("Name",  ColumnDataType.String, "String","nvarchar", true,50, true, "Language", 100),
        new ColumnDefinition("IsActive",  ColumnDataType.Bool, "Boolean","bit", false,0, false, "Active", 100)
    }
        };




        return objectToMap;
    }

}
