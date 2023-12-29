// Auto generated with CrudCreator v1.0.0
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CrudUi.Pages.Features.Config.Models;

[Table("TranslationsTypes", Schema = "dbo")]
public class TranslationsType
{
    [Key]
    [Required(ErrorMessage = "translation_type is required")]
    [DisplayFormat(NullDisplayText = "Empty", ConvertEmptyStringToNull = true)]
    [StringLength(20)]
    public string translation_type { get; set; }
    public bool IsActive { get; set; }
    [StringLength(255)]
    public string Description { get; set; }
}
