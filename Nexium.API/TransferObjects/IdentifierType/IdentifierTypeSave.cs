using System.ComponentModel.DataAnnotations;

namespace Nexium.API.TransferObjects.IdentifierType;

public class IdentifierTypeSave
{
    [Required] [MaxLength(50)] public string Label { get; set; }
}