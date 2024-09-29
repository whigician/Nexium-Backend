using Nexium.API.Entities;
using Nexium.API.TransferObjects.TargetMarket;
using Riok.Mapperly.Abstractions;

namespace Nexium.API.Configuration;

[Mapper]
public partial class TargetMarketMapper
{
    public partial TargetMarket MapToTargetMarket(TargetMarketSave targetMarketSave);
    public partial TargetMarketView MapToTargetMarketView(TargetMarket targetMarket);
}