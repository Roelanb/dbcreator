IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DefectQualityLevels]') AND type in (N'U'))  return;
CREATE TABLE [dbo].[DefectQualityLevels]
(
    [DefectQualityLevel] nvarchar(50) NOT NULL,
    [English_Desc] nvarchar(50) NOT NULL,
    [QALevelRanking] int NOT NULL,
    [ReadOnly] bit NULL,
    [IsActive] bit NULL,
    CONSTRAINT [PK_DefectQualityLevels] PRIMARY KEY CLUSTERED ([DefectQualityLevel] ASC)
)
GO
GRANT SELECT, INSERT, UPDATE, DELETE ON [dbo].[DefectQualityLevels] TO [app_datawriters]
GO
GRANT SELECT ON [dbo].[DefectQualityLevels] TO [app_datareaders]
GO
