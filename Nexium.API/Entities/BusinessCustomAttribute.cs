using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexium.API.Entities;

public class BusinessCustomAttribute
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required] [MaxLength(50)] public string Name { get; set; }

    [Required] [MaxLength(25)] public string Type { get; set; }

    [Required] [MaxLength(120)] public string Value { get; set; }

    public Business Business { get; set; }
    public long BusinessId { get; set; }
}