IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[csp_dlm_DefectPieceRefs_delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[csp_dlm_DefectPieceRefs_delete]
GO
CREATE PROCEDURE [dbo].[csp_dlm_DefectPieceRefs_delete]
    @PieceRef nvarchar(20)
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM DefectPieceRefs
    WHERE
        [PieceRef] = @PieceRef
END
GO
GRANT EXECUTE ON [dbo].[csp_dlm_DefectPieceRefs_delete] TO [app_datawriters]
GO
