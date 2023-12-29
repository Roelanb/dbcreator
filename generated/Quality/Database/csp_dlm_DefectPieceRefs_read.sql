IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[csp_dlm_DefectPieceRefs_read]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[csp_dlm_DefectPieceRefs_read]
GO
CREATE PROCEDURE [dbo].[csp_dlm_DefectPieceRefs_read]
    @PieceRef nvarchar(20) = null
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        [PieceRef],
        [OnBody],
        [OnCap],
        [English_Desc],
        [ReadOnly],
        [Defect_Desc],
        [IsActive]
    FROM
        DefectPieceRefs
    WHERE
       ( [PieceRef]= @PieceRef OR @PieceRef IS NULL)
END
GO
GRANT EXECUTE ON [dbo].[csp_dlm_DefectPieceRefs_read] TO [app_datawriters]
GO
GRANT EXECUTE ON [dbo].[csp_dlm_DefectPieceRefs_read] TO [app_datareaders]
GO
