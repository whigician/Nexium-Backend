using Nexium.API.Entities;
using Nexium.API.TransferObjects.Industry;
using Riok.Mapperly.Abstractions;

namespace Nexium.API.Configuration;

[Mapper]
public partial class IndustryMapper
{
    public partial Industry MapToIndustry(IndustrySave industrySave);
    public partial IndustryView MapToIndustryView(Industry industry);
}