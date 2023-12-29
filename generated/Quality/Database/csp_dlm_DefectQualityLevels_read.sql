IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[csp_dlm_DefectQualityLevels_read]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[csp_dlm_DefectQualityLevels_read]
GO
CREATE PROCEDURE [dbo].[csp_dlm_DefectQualityLevels_read]
    @DefectQualityLevel nvarchar(50) = null
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        [DefectQualityLevel],
        [English_Desc],
        [QALevelRanking],
        [ReadOnly],
        [IsActive]
    FROM
        DefectQualityLevels
    WHERE
       ( [DefectQualityLevel]= @DefectQualityLevel OR @DefectQualityLevel IS NULL)
END
GO
GRANT EXECUTE ON [dbo].[csp_dlm_DefectQualityLevels_read] TO [app_datawriters]
GO
GRANT EXECUTE ON [dbo].[csp_dlm_DefectQualityLevels_read] TO [app_datareaders]
GO
