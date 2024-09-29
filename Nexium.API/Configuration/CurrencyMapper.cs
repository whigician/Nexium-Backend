using Nexium.API.Entities;
using Nexium.API.TransferObjects.Currency;
using Riok.Mapperly.Abstractions;

namespace Nexium.API.Configuration;

[Mapper]
public partial class CurrencyMapper
{
    public partial Currency MapToCurrency(CurrencySave currencySave);
    public partial CurrencyView MapToCurrencyView(Currency currency);
}