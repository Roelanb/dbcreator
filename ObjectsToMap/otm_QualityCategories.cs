public static partial class ObjectsToMapData
{
    public static ObjectToMap QualityCategories(string connectionString)
    {
        var objectToMap = new ObjectToMap
        {
            Name = "QualityCategories",
            TableName = "QualityCategories",
            EntityName = "QualityCategoryModel",
            FolderLevel1 = "Quality",
            UiFolderLevel1 = "Config",
            ConnectionString = connectionString,
            NameSpace = "DataService.Config.Features.Quality",
            UiNameSpace = "MesExplorer",

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
            // *** COLUMNS START ***
PrimaryKey = "QualityCategory",
Columns = new List<ColumnDefinition>
{
new ColumnDefinition("QualityCategory", ColumnDataType.String, "String","nvarchar", true, 30, true, "QualityCategory", 250),
new ColumnDefinition("description", ColumnDataType.String, "String","nvarchar", false, 4000, true, "description", 250),
new ColumnDefinition("IsActive", ColumnDataType.Bool, "Boolean","bit", false, null, true, "IsActive", 100),
},
// *** COLUMNS END ***
            SourceSqlTableCreateCommand = @"CREATE TABLE [dbo].[QualityCategories](
	[QualityCategory] [nvarchar](30) NOT NULL,
	[description] [nvarchar](4000) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Qdm_ProductQualityCategories] PRIMARY KEY CLUSTERED 
(
	[QualityCategory] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]"
        };



        return objectToMap;
    }

}
