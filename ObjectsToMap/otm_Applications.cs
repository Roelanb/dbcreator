public static partial class ObjectsToMapData
{
    public static ObjectToMap Applications(string connectionString)
    {
        var objectToMap = new ObjectToMap
        {
            Name = "Applications",
            TableName = "Applications",
            EntityName = "Application",
            FolderLevel1 = "Translations",
            UiFolderLevel1 = "Config",
            ConnectionString = connectionString,
            NameSpace = "DataService.Config.Features.Translations",
            UiNameSpace = "MesExplorer",

            ExampleKeyValue = "TST",
            ExampleObjectListJson = @"[
                {
                    ""name"": ""TST"",
                    ""description"": ""TestApplication"",
                    ""isActive"": true
             },
                {
                    ""name"": ""TST2"",
                    ""description"": ""TestApplication2"",
                    ""isActive"": true
                }
                ]",
            PrimaryKey = "Name",
            Columns = new List<ColumnDefinition>
            {
                new ColumnDefinition("Name", ColumnDataType.String, "String","nvarchar", true,50, true, "Application", 50),
                new ColumnDefinition("Description", ColumnDataType.String, "String","nvarchar", false,500, false, "Description", 250),
                new ColumnDefinition("IsActive", ColumnDataType.Bool, "Boolean","bit", false,0, false, "Active", 100)
            }
        };

       


        return objectToMap;
    }

}
