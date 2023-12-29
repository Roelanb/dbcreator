IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[csp_dlm_TranslationsTypes_update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[csp_dlm_TranslationsTypes_update]
GO
CREATE PROCEDURE [dbo].[csp_dlm_TranslationsTypes_update]
    @translation_type nvarchar(20),
    @IsActive bit,
    @Description nvarchar(255)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE TranslationsTypes SET
        [translation_type] = @translation_type,
        [IsActive] = @IsActive,
        [Description] = @Description
    WHERE
        [translation_type] = @translation_type

    SELECT
        [translation_type],
        [IsActive],
        [Description]
    FROM
        TranslationsTypes
    WHERE
        [translation_type] = @translation_type
END
GO
GRANT EXECUTE ON [dbo].[csp_dlm_TranslationsTypes_update] TO [app_datawriters]
GO
