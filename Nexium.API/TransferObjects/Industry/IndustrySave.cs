using System.ComponentModel.DataAnnotations;

namespace Nexium.API.TransferObjects.Industry;

public class IndustrySave
{
    [Required] [MaxLength(50)] public string Label { get; set; }
}