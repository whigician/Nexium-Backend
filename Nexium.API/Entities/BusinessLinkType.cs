using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexium.API.Entities;

public class BusinessLinkType
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public byte Id { get; set; }

    [Required] [MaxLength(50)] public string Label { get; set; }
    [MaxLength(250)] public string OriginalLogoUrl { get; set; }
    [MaxLength(250)] public string ResizedLogoUrl { get; set; }
    public ICollection<BusinessLink> BusinessLinks { get; set; } = new List<BusinessLink>();
}