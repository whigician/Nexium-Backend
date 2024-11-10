using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexium.API.Entities;

public class BusinessLink
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [MaxLength(250)] public string Url { get; set; }
    public Business Business { get; set; }
    public long BusinessId { get; set; }
    public BusinessLinkType BusinessLinkType { get; set; }
    public byte BusinessLinkTypeId { get; set; }
}