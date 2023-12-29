public static partial class ObjectsToMapData
{
    public static ObjectToMap DefectQualityLevels(string connectionString)
    {
        var objectToMap = new ObjectToMap
        {
            Name = "DefectQualityLevels",
            TableName = "DefectQualityLevels",
            EntityName = "DefectQualityLevelModel",
            FolderLevel1 = "Quality",
            UiFolderLevel1 = "Config",
            ConnectionString = connectionString,
            NameSpace = "DataService.Config.Features.Quality",
            UiNameSpace = "CrudUi.Pages",

            ExampleKeyValue = "TST",
            ExampleObjectListJson = @"[
                {
                    ""DefectQualityLevel"": ""L1"",
                    ""English_Desc"": ""Level 1"",
                    ""QALevelRanking"": 1,
                    ""ReadOnly"": true,
                    ""isActive"": true
             },
                {
                    ""DefectQualityLevel"": ""L2"",
                    ""English_Desc"": ""Level 2"",
                    ""QALevelRanking"": 2,
                    ""ReadOnly"": true,
                    ""isActive"": true
                }
               
                ]",
            PrimaryKey = "DefectQualityLevel",
            Columns = new List<ColumnDefinition>
            {
                new ColumnDefinition("DefectQualityLevel", ColumnDataType.String, "String","nvarchar", true, 50, true, "Defect Quality Level", 250),
                new ColumnDefinition("English_Desc", ColumnDataType.String, "String","nvarchar", false, 50, true, "Description", 250),
                new ColumnDefinition("QALevelRanking", ColumnDataType.Int, "Int16","int", false, null, true, "QALevelRanking", 250),
                new ColumnDefinition("ReadOnly", ColumnDataType.Bool, "Boolean","bit", false,0, false, "ReadOnly", 100),
                new ColumnDefinition("IsActive", ColumnDataType.Bool, "Boolean","bit", false,0, false, "Active", 100)
            }
        };



        return objectToMap;
    }

}
