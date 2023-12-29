IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Languages]') AND type in (N'U'))  return;
CREATE TABLE [dbo].[Languages]
(
    [Name] nvarchar(50) NOT NULL,
    [IsActive] bit NULL,
    CONSTRAINT [PK_Languages] PRIMARY KEY CLUSTERED ([Name] ASC)
)
GO
GRANT SELECT, INSERT, UPDATE, DELETE ON [dbo].[Languages] TO [app_datawriters]
GO
GRANT SELECT ON [dbo].[Languages] TO [app_datareaders]
GO
