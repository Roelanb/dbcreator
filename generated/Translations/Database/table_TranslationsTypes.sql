IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TranslationsTypes]') AND type in (N'U'))  return;
CREATE TABLE [dbo].[TranslationsTypes]
(
    [translation_type] nvarchar(20) NOT NULL,
    [IsActive] bit NULL,
    [Description] nvarchar(255) NULL,
    CONSTRAINT [PK_TranslationsTypes] PRIMARY KEY CLUSTERED ([translation_type] ASC)
)
GO
GRANT SELECT, INSERT, UPDATE, DELETE ON [dbo].[TranslationsTypes] TO [app_datawriters]
GO
GRANT SELECT ON [dbo].[TranslationsTypes] TO [app_datareaders]
GO
