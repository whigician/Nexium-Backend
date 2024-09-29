using System.ComponentModel.DataAnnotations;

namespace Nexium.API.TransferObjects.ContactType;

public class ContactTypeSave
{
    [Required] [MaxLength(50)] public string Label { get; set; }
}