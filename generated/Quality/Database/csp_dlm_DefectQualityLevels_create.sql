IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[csp_dlm_DefectQualityLevels_create]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[csp_dlm_DefectQualityLevels_create]
GO
CREATE PROCEDURE [dbo].[csp_dlm_DefectQualityLevels_create]
    @DefectQualityLevel nvarchar(50),
    @English_Desc nvarchar(50),
    @QALevelRanking int,
    @ReadOnly bit,
    @IsActive bit
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO DefectQualityLevels (
[DefectQualityLevel],[English_Desc],[QALevelRanking],[ReadOnly],[IsActive])
    VALUES (
        @DefectQualityLevel,
        @English_Desc,
        @QALevelRanking,
        @ReadOnly,
        @IsActive
    );
    SELECT
        [DefectQualityLevel],
        [English_Desc],
        [QALevelRanking],
        [ReadOnly],
        [IsActive]
    FROM
        DefectQualityLevels
    WHERE
        [DefectQualityLevel]= @DefectQualityLevel
END
GO
GRANT EXECUTE ON [dbo].[csp_dlm_DefectQualityLevels_create] TO [app_datawriters]
GO
