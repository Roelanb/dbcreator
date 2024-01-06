public static partial class ObjectsToMapData
{
    public static List<ObjectToMap> GetObjectsToMap()
    {
        var objectsToMap = new List<ObjectToMap>();

        var connectionString = "server=.;database=MES;Persist Security Info=False;Integrated Security=True;TrustServerCertificate=True;";            

        objectsToMap.Add(DefectQualityLevels(connectionString));
        objectsToMap.Add(QualityCategories(connectionString));
        objectsToMap.Add(DefectPieceRefs(connectionString));
        objectsToMap.Add(LogoutIdleTimer(connectionString));
        objectsToMap.Add(ManufacturingType(connectionString));
        
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
                new ColumnDefinition("Name", ColumnDataType.String, "String","nvarchar", true,50, true, "Application", 50),
                new ColumnDefinition("Description", ColumnDataType.String, "String","nvarchar", false,500, false, "Description", 250),
                new ColumnDefinition("IsActive", ColumnDataType.Bool, "Boolean","bit", false,0, false, "Active", 100)
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
        new ColumnDefinition("Name",  ColumnDataType.String, "String","nvarchar", true,50, true, "Language", 100),
        new ColumnDefinition("IsActive",  ColumnDataType.Bool, "Boolean","bit", false,0, false, "Active", 100)
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
        new ColumnDefinition("translation_type",  ColumnDataType.String, "String","nvarchar", true,20, true, "Type", 100),
        new ColumnDefinition("IsActive",  ColumnDataType.Bool, "Boolean","bit", false,0, false, "Active", 100),
        new ColumnDefinition("Description",  ColumnDataType.String, "String","nvarchar", false,255, false, "Description", 100)
    }
        });

        foreach (var objectToMap in objectsToMap)
        {
            objectToMap.RecalculateColumnWidths();
        }

        return objectsToMap;
    }

}
