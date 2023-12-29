IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[csp_dlm_Applications_create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[csp_dlm_Applications_create]
GO
CREATE PROCEDURE [dbo].[csp_dlm_Applications_create]
    @Name nvarchar(50),
    @Description nvarchar(500),
    @IsActive bit
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Applications (
[Name],[Description],[IsActive])
    VALUES (
        @Name,
        @Description,
        @IsActive
    );
    SELECT
        [Name],
        [Description],
        [IsActive]
    FROM
        Applications
    WHERE
        [Name]= @Name
END
GO
GRANT EXECUTE ON [dbo].[csp_dlm_Applications_create] TO [app_datawriters]
GO
