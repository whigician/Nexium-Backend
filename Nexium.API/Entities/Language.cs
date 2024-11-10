using System.ComponentModel.DataAnnotations;

namespace Nexium.API.Entities;

public class Language
{
    [Key] [MaxLength(2)] public string Code { get; set; }
    [Required] [MaxLength(25)] public string Name { get; set; }
    public ICollection<Business> Businesses { get; set; } = new List<Business>();
    public ICollection<TranslationMapping> TranslationMappings { get; set; } = new List<TranslationMapping>();
}