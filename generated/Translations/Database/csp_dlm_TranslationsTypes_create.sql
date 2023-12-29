IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[csp_dlm_TranslationsTypes_create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[csp_dlm_TranslationsTypes_create]
GO
CREATE PROCEDURE [dbo].[csp_dlm_TranslationsTypes_create]
    @translation_type nvarchar(20),
    @IsActive bit,
    @Description nvarchar(255)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO TranslationsTypes (
[translation_type],[IsActive],[Description])
    VALUES (
        @translation_type,
        @IsActive,
        @Description
    );
    SELECT
        [translation_type],
        [IsActive],
        [Description]
    FROM
        TranslationsTypes
    WHERE
        [translation_type]= @translation_type
END
GO
GRANT EXECUTE ON [dbo].[csp_dlm_TranslationsTypes_create] TO [app_datawriters]
GO
