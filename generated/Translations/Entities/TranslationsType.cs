using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataService.Config.Features.Translations.Entities;

[Table("TranslationsTypes", Schema = "dbo")]
public class TranslationsType
{
    [Key]
    [Required]
    [StringLength(20)]
    public string translation_type { get; set; }
    public bool IsActive { get; set; }
    [StringLength(255)]
    public string Description { get; set; }
}
