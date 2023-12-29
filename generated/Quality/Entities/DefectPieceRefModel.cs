using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataService.Config.Features.Quality.Entities;

[Table("DefectPieceRefs", Schema = "dbo")]
public class DefectPieceRefModel
{
    [Key]
    [Required]
    [StringLength(20)]
    public string PieceRef { get; set; }
    [Required]
    public bool OnBody { get; set; }
    [Required]
    public bool OnCap { get; set; }
    [Required]
    [StringLength(50)]
    public string English_Desc { get; set; }
    public bool ReadOnly { get; set; }
    [Required]
    [StringLength(50)]
    public string Defect_Desc { get; set; }
    public bool IsActive { get; set; }
}
