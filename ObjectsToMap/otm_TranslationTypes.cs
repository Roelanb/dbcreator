public static partial class ObjectsToMapData
{
    public static ObjectToMap TranslationTypes(string connectionString)
    {
        var objectToMap = new ObjectToMap
        {
            Name = "TranslationTypes",
            TableName = "TranslationTypes",
            EntityName = "TranslationType",
            FolderLevel1 = "Translations",
            UiFolderLevel1 = "Config",
            ConnectionString = connectionString,
            NameSpace = "DataService.Config.Features.Translations",
            UiNameSpace = "MesExplorer",

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
        };




        return objectToMap;
    }

}
