IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[csp_dlm_Applications_update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[csp_dlm_Applications_update]
GO
CREATE PROCEDURE [dbo].[csp_dlm_Applications_update]
    @Name nvarchar(50),
    @Description nvarchar(500),
    @IsActive bit
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Applications SET
        [Name] = @Name,
        [Description] = @Description,
        [IsActive] = @IsActive
    WHERE
        [Name] = @Name

    SELECT
        [Name],
        [Description],
        [IsActive]
    FROM
        Applications
    WHERE
        [Name] = @Name
END
GO
GRANT EXECUTE ON [dbo].[csp_dlm_Applications_update] TO [app_datawriters]
GO
