IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Applications]') AND type in (N'U'))  return;
CREATE TABLE [dbo].[Applications]
(
    [Name] nvarchar(50) NOT NULL,
    [Description] nvarchar(500) NULL,
    [IsActive] bit NULL,
    CONSTRAINT [PK_Applications] PRIMARY KEY CLUSTERED ([Name] ASC)
)
GO
GRANT SELECT, INSERT, UPDATE, DELETE ON [dbo].[Applications] TO [app_datawriters]
GO
GRANT SELECT ON [dbo].[Applications] TO [app_datareaders]
GO
