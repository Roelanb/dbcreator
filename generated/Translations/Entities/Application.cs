using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataService.Config.Features.Translations.Entities;

[Table("Applications", Schema = "dbo")]
public class Application
{
    [Key]
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    [StringLength(500)]
    public string Description { get; set; }
    public bool IsActive { get; set; }
}
