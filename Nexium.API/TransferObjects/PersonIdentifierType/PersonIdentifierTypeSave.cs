using System.ComponentModel.DataAnnotations;

namespace Nexium.API.TransferObjects.PersonIdentifierType;

public class PersonIdentifierTypeSave
{
    [Required] [MaxLength(50)] public string Label { get; set; }
}