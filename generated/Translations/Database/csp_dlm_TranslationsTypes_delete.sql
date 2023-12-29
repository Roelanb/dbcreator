IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[csp_dlm_TranslationsTypes_delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[csp_dlm_TranslationsTypes_delete]
GO
CREATE PROCEDURE [dbo].[csp_dlm_TranslationsTypes_delete]
    @translation_type nvarchar(20)
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM TranslationsTypes
    WHERE
        [translation_type] = @translation_type
END
GO
GRANT EXECUTE ON [dbo].[csp_dlm_TranslationsTypes_delete] TO [app_datawriters]
GO
