using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexium.API.Entities;

public class Industry
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public short Id { get; set; }

    [Required] [MaxLength(50)] public string Label { get; set; }

    public List<Business> Businesses { get; set; }
    public List<IndustryTranslation> Translations { get; set; }
}