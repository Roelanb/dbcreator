IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[csp_dlm_Languages_update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[csp_dlm_Languages_update]
GO
CREATE PROCEDURE [dbo].[csp_dlm_Languages_update]
    @Name nvarchar(50),
    @IsActive bit
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Languages SET
        [Name] = @Name,
        [IsActive] = @IsActive
    WHERE
        [Name] = @Name

    SELECT
        [Name],
        [IsActive]
    FROM
        Languages
    WHERE
        [Name] = @Name
END
GO
GRANT EXECUTE ON [dbo].[csp_dlm_Languages_update] TO [app_datawriters]
GO
