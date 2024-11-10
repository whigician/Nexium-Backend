using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexium.API.Entities;

public class Role
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public byte Id { get; set; }

    [Required] [MaxLength(50)] public string Label { get; set; }
    public ICollection<BusinessRole> BusinessRoles { get; set; } = new List<BusinessRole>();
}