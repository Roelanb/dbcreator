using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataService.Config.Features.Translations.Entities;

[Table("Languages", Schema = "dbo")]
public class Language
{
    [Key]
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    public bool IsActive { get; set; }
}
