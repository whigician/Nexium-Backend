using System.ComponentModel.DataAnnotations;

namespace Nexium.API.TransferObjects.Currency;

public class CurrencySave
{
    [Required] [MaxLength(50)] public string Label { get; set; }
}