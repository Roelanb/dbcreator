IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[csp_dlm_TranslationsTypes_read]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[csp_dlm_TranslationsTypes_read]
GO
CREATE PROCEDURE [dbo].[csp_dlm_TranslationsTypes_read]
    @translation_type nvarchar(20) = null
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        [translation_type],
        [IsActive],
        [Description]
    FROM
        TranslationsTypes
    WHERE
       ( [translation_type]= @translation_type OR @translation_type IS NULL)
END
GO
GRANT EXECUTE ON [dbo].[csp_dlm_TranslationsTypes_read] TO [app_datawriters]
GO
GRANT EXECUTE ON [dbo].[csp_dlm_TranslationsTypes_read] TO [app_datareaders]
GO
