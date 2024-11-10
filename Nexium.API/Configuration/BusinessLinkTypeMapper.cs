using Nexium.API.Entities;
using Nexium.API.TransferObjects.BusinessLinkType;
using Riok.Mapperly.Abstractions;

namespace Nexium.API.Configuration;

[Mapper]
public partial class BusinessLinkTypeMapper
{
    public partial BusinessLinkType MapToBusinessLinkType(BusinessLinkTypeSave businessLinkTypeSave);
    public partial BusinessLinkTypeView MapToBusinessLinkTypeView(BusinessLinkType businessLinkType);
}