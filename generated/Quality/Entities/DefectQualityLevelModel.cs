using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataService.Config.Features.Quality.Entities;

[Table("DefectQualityLevels", Schema = "dbo")]
public class DefectQualityLevelModel
{
    [Key]
    [Required]
    [StringLength(50)]
    public string DefectQualityLevel { get; set; }
    [Required]
    [StringLength(50)]
    public string English_Desc { get; set; }
    [Required]
    public int QALevelRanking { get; set; }
    public bool ReadOnly { get; set; }
    public bool IsActive { get; set; }
}
