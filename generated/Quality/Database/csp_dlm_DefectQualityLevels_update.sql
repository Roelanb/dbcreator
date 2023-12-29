IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[csp_dlm_DefectQualityLevels_update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[csp_dlm_DefectQualityLevels_update]
GO
CREATE PROCEDURE [dbo].[csp_dlm_DefectQualityLevels_update]
    @DefectQualityLevel nvarchar(50),
    @English_Desc nvarchar(50),
    @QALevelRanking int,
    @ReadOnly bit,
    @IsActive bit
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE DefectQualityLevels SET
        [DefectQualityLevel] = @DefectQualityLevel,
        [English_Desc] = @English_Desc,
        [QALevelRanking] = @QALevelRanking,
        [ReadOnly] = @ReadOnly,
        [IsActive] = @IsActive
    WHERE
        [DefectQualityLevel] = @DefectQualityLevel

    SELECT
        [DefectQualityLevel],
        [English_Desc],
        [QALevelRanking],
        [ReadOnly],
        [IsActive]
    FROM
        DefectQualityLevels
    WHERE
        [DefectQualityLevel] = @DefectQualityLevel
END
GO
GRANT EXECUTE ON [dbo].[csp_dlm_DefectQualityLevels_update] TO [app_datawriters]
GO
