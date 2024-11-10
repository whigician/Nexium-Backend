using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexium.API.Entities;

public class PersonIdentifier
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required] [MaxLength(30)] public string Value { get; set; }

    [MaxLength(250)] public string DocumentUrl { get; set; }

    public PersonIdentifierType PersonIdentifierType { get; set; }
    public byte PersonIdentifierTypeId { get; set; }
    public BusinessMember BusinessMember { get; set; }
    public long? InternalPersonId { get; set; }
}