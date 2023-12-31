public static partial class ObjectsToMapData
{
    public static ObjectToMap LogoutIdleTimer(string connectionString)
    {
        var objectToMap = new ObjectToMap
        {
            Name = "LogoutIdleTimer",
            TableName = "LogoutIdleTimer",
            EntityName = "LogoutIdleTimerModel",
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
PrimaryKey = "id",
Columns = new List<ColumnDefinition>
{
new ColumnDefinition("id", ColumnDataType.Int, "Int32","int", true, null, true, "id", 250),
new ColumnDefinition("Application", ColumnDataType.String, "String","nvarchar", true, 50, true, "Application", 250),
new ColumnDefinition("Site", ColumnDataType.String, "String","nvarchar", false, 50, true, "Site", 250),
new ColumnDefinition("Area", ColumnDataType.String, "String","nvarchar", false, 50, true, "Area", 250),
new ColumnDefinition("Seconds", ColumnDataType.Int, "Int32","int", false, null, true, "Seconds", 250),
new ColumnDefinition("IsActive", ColumnDataType.Bool, "Boolean","bit", false, null, true, "IsActive", 250),
new ColumnDefinition("LastEditDate", ColumnDataType.DateTime, "DateTime","datetime", true, null, true, "LastEditDate", 250),
new ColumnDefinition("UserName", ColumnDataType.String, "String","nvarchar", true, 255, true, "UserName", 250),
},
// *** COLUMNS END ***
            SourceSqlTableCreateCommand = @"CREATE TABLE [dbo].[LogoutIdleTimer](
                                [id] [int] IDENTITY(1,1) NOT NULL,
                                [Application] [nvarchar](50) NULL,
                                [Site] [nvarchar](50) NOT NULL,
                                [Area] [nvarchar](50) NOT NULL,
                                [Seconds] [int] NOT NULL,
                                [IsActive] [bit] NOT NULL,
                                [LastEditDate] [datetime] NULL,
                                [UserName] [nvarchar](255) NULL,
                            CONSTRAINT [PK__LogoutIdleTimer] PRIMARY KEY CLUSTERED 
                            (
                                [id] ASC
                            )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
                            ) ON [PRIMARY]"
        };



        return objectToMap;
    }

}

