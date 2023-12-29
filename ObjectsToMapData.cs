public static class ObjectsToMapData
{
    public static List<ObjectToMap> GetObjectsToMap()
    {
        var objectsToMap = new List<ObjectToMap>();

        var connectionString = "server=.;database=MES;Persist Security Info=False;Integrated Security=True;TrustServerCertificate=True;";            


        objectsToMap.Add(new ObjectToMap
        {
            Name = "Applications",
            TableName = "Applications",
            EntityName = "Application",
            FolderLevel1 = "Translations",
            UiFolderLevel1 = "Config",
            ConnectionString = connectionString,
            NameSpace = "DataService.Config.Features.Translations",
            UiNameSpace = "CrudUi.Pages",

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
                new ColumnDefinition("Name", "string", "String","nvarchar", true,50, true, "Application", 100),
                new ColumnDefinition("Description", "string", "String","nvarchar", false,500, false, "Description", 100),
                new ColumnDefinition("IsActive", "bool", "Boolean","bit", false,0, false, "Active", 100)
            }
        });

        objectsToMap.Add(new ObjectToMap
        {
            Name = "Languages",
            TableName = "Languages",
            EntityName = "Language",
            FolderLevel1 = "Translations",
            UiFolderLevel1 = "Config",
            ConnectionString = connectionString,
            NameSpace = "DataService.Config.Features.Translations",
            UiNameSpace = "CrudUi.Pages",

            ExampleKeyValue = "GERMAN",
            ExampleObjectListJson = @"[
                {
                    ""name"": ""GERMAN"",
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
        new ColumnDefinition("Name", "string", "String","nvarchar", true,50, true, "Language", 100),
        new ColumnDefinition("IsActive", "bool", "Boolean","bit", false,0, false, "Active", 100)
    }
        });

        objectsToMap.Add(new ObjectToMap
        {
            Name = "TranslationsTypes",
            TableName = "TranslationsTypes",
            EntityName = "TranslationsType",
            FolderLevel1 = "Translations",
            UiFolderLevel1 = "Config",
            ConnectionString = connectionString,
            NameSpace = "DataService.Config.Features.Translations",
            UiNameSpace = "CrudUi.Pages",

            ExampleKeyValue = "T1",
            ExampleObjectListJson = @"[
        {
            ""translation_type"": ""T1"",
            ""description"": ""TestTranslationType"",
            ""isActive"": true
        },
        {
            ""translation_type"": ""T2"",
            ""description"": ""TestTranslationType2"",
            ""isActive"": true
        }
        ]",

            PrimaryKey = "translation_type",
            Columns = new List<ColumnDefinition>
    {
        new ColumnDefinition("translation_type", "string", "String","nvarchar", true,20, true, "Type", 100),
        new ColumnDefinition("IsActive", "bool", "Boolean","bit", false,0, false, "Active", 100),
        new ColumnDefinition("Description", "string", "String","nvarchar", false,255, false, "Description", 100)
    }
        });
        return objectsToMap;
    }

}
