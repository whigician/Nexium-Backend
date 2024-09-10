using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexium.API.Entities;

public class BusinessIdentifier
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required] [MaxLength(50)] public string Value { get; set; }
    public Business Business { get; set; }
    public long BusinessId { get; set; }
    public IdentifierType IdentifierType { get; set; }
    public sbyte IdentifierTypeId { get; set; }
}