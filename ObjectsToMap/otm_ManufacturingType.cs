public static partial class ObjectsToMapData
{
    public static ObjectToMap ManufacturingType(string connectionString)
    {
        var objectToMap = new ObjectToMap
        {
            Name = "ManufacturingType",
            TableName = "ManufacturingType",
            EntityName = "ManufacturingTypeModel",
            FolderLevel1 = "MasterData",
            UiFolderLevel1 = "Config",
            ConnectionString = connectionString,
            NameSpace = "DataService.Config.Features.MasterData",
            UiNameSpace = "CrudUi.Pages",

            ExampleKeyValue = "TST",
            ExampleObjectListJson = @"[
                {
                    ""id"": ""id1"",
                    ""Application"": ""Application1"",
                    ""Site"": ""Site1"",
                    ""Area"": ""Area1"",
                    ""Seconds"": 1,
                    ""IsActive"": true,
                    ""LastEditDate"": ""2021-01-01T00:00:00"",
                    ""UserName"": ""UserName1""

             },
                {
                    ""id"": ""id2"",
                    ""Application"": ""Application2"",
                    ""Site"": ""Site2"",
                    ""Area"": ""Area2"",
                    ""Seconds"": 2,
                    ""IsActive"": true,
                    ""LastEditDate"": ""2021-01-01T00:00:00"",
                    ""UserName"": ""UserName2""
                ]",

            // *** COLUMNS START ***
PrimaryKey = "manufacturingtype",
Columns = new List<ColumnDefinition>
{
new ColumnDefinition("manufacturingtype", ColumnDataType.String, "String","nvarchar", true, 50, true, "manufacturingtype", 250),
new ColumnDefinition("description", ColumnDataType.String, "String","nvarchar", true, 150, true, "description", 250),
new ColumnDefinition("description2", ColumnDataType.String, "String","nvarchar", true, 150, true, "description2", 250),
new ColumnDefinition("HandlingStepNeededBefore", ColumnDataType.String, "String","nvarchar", true, 20, true, "HandlingStepNeededBefore", 250),
new ColumnDefinition("StockReservedTo", ColumnDataType.String, "String","nvarchar", true, 20, true, "StockReservedTo", 250),
new ColumnDefinition("isactive", ColumnDataType.Bool, "Boolean","bit", false, null, true, "isactive", 250),
new ColumnDefinition("ManufacturingTypeGroup", ColumnDataType.String, "String","nvarchar", false, 20, true, "ManufacturingTypeGroup", 250),
},
// *** COLUMNS END ***
            SourceSqlTableCreateCommand = @"CREATE TABLE [dbo].[ManufacturingType](
	[manufacturingtype] [nvarchar](50) NOT NULL,
	[description] [nvarchar](150) NULL,
	[description2] [nvarchar](150) NULL,
	[HandlingStepNeededBefore] [nvarchar](20) NULL,
	[StockReservedTo] [nvarchar](20) NULL,
	[isactive] [bit] NOT NULL,
	[ManufacturingTypeGroup] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_ManufacturingType] PRIMARY KEY CLUSTERED 
(
	[manufacturingtype] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]"
        };



        return objectToMap;
    }

}

