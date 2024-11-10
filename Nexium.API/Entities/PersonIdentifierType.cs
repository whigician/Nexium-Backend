using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexium.API.Entities;

public class PersonIdentifierType
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public byte Id { get; set; }

    [Required] [MaxLength(50)] public string Label { get; set; }
    public ICollection<PersonIdentifier> PersonIdentifiers { get; set; } = new List<PersonIdentifier>();
}