IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[csp_dlm_Languages_delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[csp_dlm_Languages_delete]
GO
CREATE PROCEDURE [dbo].[csp_dlm_Languages_delete]
    @Name nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM Languages
    WHERE
        [Name] = @Name
END
GO
GRANT EXECUTE ON [dbo].[csp_dlm_Languages_delete] TO [app_datawriters]
GO