IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[csp_dlm_DefectPieceRefs_create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[csp_dlm_DefectPieceRefs_create]
GO
CREATE PROCEDURE [dbo].[csp_dlm_DefectPieceRefs_create]
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
    INSERT INTO DefectPieceRefs (
[PieceRef],[OnBody],[OnCap],[English_Desc],[ReadOnly],[Defect_Desc],[IsActive])
    VALUES (
        @PieceRef,
        @OnBody,
        @OnCap,
        @English_Desc,
        @ReadOnly,
        @Defect_Desc,
        @IsActive
    );
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
        [PieceRef]= @PieceRef
END
GO
GRANT EXECUTE ON [dbo].[csp_dlm_DefectPieceRefs_create] TO [app_datawriters]
GO
