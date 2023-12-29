IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DefectPieceRefs]') AND type in (N'U'))  return;
CREATE TABLE [dbo].[DefectPieceRefs]
(
    [PieceRef] nvarchar(20) NOT NULL,
    [OnBody] bit NOT NULL,
    [OnCap] bit NOT NULL,
    [English_Desc] nvarchar(50) NOT NULL,
    [ReadOnly] bit NULL,
    [Defect_Desc] nvarchar(50) NOT NULL,
    [IsActive] bit NULL,
    CONSTRAINT [PK_DefectPieceRefs] PRIMARY KEY CLUSTERED ([PieceRef] ASC)
)
GO
GRANT SELECT, INSERT, UPDATE, DELETE ON [dbo].[DefectPieceRefs] TO [app_datawriters]
GO
GRANT SELECT ON [dbo].[DefectPieceRefs] TO [app_datareaders]
GO
