IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[csp_dlm_Applications_read]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[csp_dlm_Applications_read]
GO
CREATE PROCEDURE [dbo].[csp_dlm_Applications_read]
    @Name nvarchar(50) = null
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        [Name],
        [Description],
        [IsActive]
    FROM
        Applications
    WHERE
       ( [Name]= @Name OR @Name IS NULL)
END
GO
GRANT EXECUTE ON [dbo].[csp_dlm_Applications_read] TO [app_datawriters]
GO
GRANT EXECUTE ON [dbo].[csp_dlm_Applications_read] TO [app_datareaders]
GO
