IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[csp_dlm_Applications_delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[csp_dlm_Applications_delete]
GO
CREATE PROCEDURE [dbo].[csp_dlm_Applications_delete]
    @Name nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM Applications
    WHERE
        [Name] = @Name
END
GO
GRANT EXECUTE ON [dbo].[csp_dlm_Applications_delete] TO [app_datawriters]
GO
