public static partial class ObjectsToMapData
{
    public static ObjectToMap DefectPieceRefs(string connectionString)
    {
        var objectToMap = new ObjectToMap
        {
            Name = "DefectPieceRefs",
            TableName = "DefectPieceRefs",
            EntityName = "DefectPieceRefModel",
            FolderLevel1 = "Quality",
            UiFolderLevel1 = "Config",
            ConnectionString = connectionString,
            NameSpace = "DataService.Config.Features.Quality",
            UiNameSpace = "MesExplorer",

            ExampleKeyValue = "TST",
            ExampleObjectListJson = @"[
                {
                    ""PieceRef"": ""PieceRef1"",
                    ""OnBody"": true,
                    ""OnCap"": false,
                    ""English_Desc"": ""Level 1"",
                    ""Defect_Desc"": ""defect 1"",
                    ""ReadOnly"": true,
                    ""isActive"": true
             },
                {
                    ""PieceRef"": ""PieceRef2"",
                    ""OnBody"": true,
                    ""OnCap"": false,
                    ""English_Desc"": ""Level 2"",
                    ""Defect_Desc"": ""defect 2"",
                    ""ReadOnly"": true,
                    ""isActive"": true
                }
                ]",
            PrimaryKey = "PieceRef",
            Columns = new List<ColumnDefinition>
            {
                new ColumnDefinition("PieceRef", ColumnDataType.String, "String","nvarchar", true, 20, true, "Defect Quality Level", 250),
                new ColumnDefinition("OnBody", ColumnDataType.Bool, "Boolean","bit", false,0, true, "OnBody", 100),
                new ColumnDefinition("OnCap", ColumnDataType.Bool, "Boolean","bit", false,0, true, "OnCap", 100),
                new ColumnDefinition("English_Desc", ColumnDataType.String, "String","nvarchar", false, 50, true, "Description", 250),
                new ColumnDefinition("ReadOnly", ColumnDataType.Bool, "Boolean","bit", false,0, false, "ReadOnly", 100),
                new ColumnDefinition("Defect_Desc", ColumnDataType.String, "String","nvarchar", false, 50, true, "QALevelRanking", 250),
                new ColumnDefinition("IsActive", ColumnDataType.Bool, "Boolean","bit", false,0, false, "Active", 100)
            }
        };



        return objectToMap;
    }

}
