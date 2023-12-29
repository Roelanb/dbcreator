IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[csp_dlm_DefectPieceRefs_update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[csp_dlm_DefectPieceRefs_update]
GO
CREATE PROCEDURE [dbo].[csp_dlm_DefectPieceRefs_update]
    @PieceRef nvarchar(20),
    @OnBody bit,
    @OnCap bit,
    @English_Desc nvarchar(50),
    @ReadOnly bit,
    @Defect_Desc nvarchar(50),
    @IsActive bit
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE DefectPieceRefs SET
        [PieceRef] = @PieceRef,
        [OnBody] = @OnBody,
        [OnCap] = @OnCap,
        [English_Desc] = @English_Desc,
        [ReadOnly] = @ReadOnly,
        [Defect_Desc] = @Defect_Desc,
        [IsActive] = @IsActive
    WHERE
        [PieceRef] = @PieceRef

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
        [PieceRef] = @PieceRef
END
GO
GRANT EXECUTE ON [dbo].[csp_dlm_DefectPieceRefs_update] TO [app_datawriters]
GO
