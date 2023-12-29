IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[csp_dlm_Languages_create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[csp_dlm_Languages_create]
GO
CREATE PROCEDURE [dbo].[csp_dlm_Languages_create]
    @Name nvarchar(50),
    @IsActive bit
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Languages (
[Name],[IsActive])
    VALUES (
        @Name,
        @IsActive
    );
    SELECT
        [Name],
        [IsActive]
    FROM
        Languages
    WHERE
        [Name]= @Name
END
GO
GRANT EXECUTE ON [dbo].[csp_dlm_Languages_create] TO [app_datawriters]
GO
