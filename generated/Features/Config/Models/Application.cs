// Auto generated with CrudCreator v1.0.0
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CrudUi.Pages.Features.Config.Models;

[Table("Applications", Schema = "dbo")]
public class Application
{
    [Key]
    [Required(ErrorMessage = "Name is required")]
    [DisplayFormat(NullDisplayText = "Empty", ConvertEmptyStringToNull = true)]
    [StringLength(50)]
    public string Name { get; set; }
    [StringLength(500)]
    public string Description { get; set; }
    public bool IsActive { get; set; }
}
