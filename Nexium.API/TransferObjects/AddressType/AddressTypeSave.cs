using System.ComponentModel.DataAnnotations;

namespace Nexium.API.TransferObjects.AddressType;

public class AddressTypeSave
{
    [Required] [MaxLength(50)] public string Label { get; set; }
}