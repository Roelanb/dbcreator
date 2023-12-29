// Auto generated with CrudCreator v1.0.0
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CrudUi.Pages.Features.Config.Models;

[Table("Languages", Schema = "dbo")]
public class Language
{
    [Key]
    [Required(ErrorMessage = "Name is required")]
    [DisplayFormat(NullDisplayText = "Empty", ConvertEmptyStringToNull = true)]
    [StringLength(50)]
    public string Name { get; set; }
    public bool IsActive { get; set; }
}