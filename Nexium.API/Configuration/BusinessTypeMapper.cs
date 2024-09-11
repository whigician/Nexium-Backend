using Nexium.API.Entities;
using Nexium.API.TransferObjects.BusinessType;
using Riok.Mapperly.Abstractions;

namespace Nexium.API.Configuration;

[Mapper]
public partial class BusinessTypeMapper
{
    public partial BusinessType MapToBusinessType(BusinessTypeSave businessTypeSave);
    public partial BusinessTypeView MapToBusinessTypeView(BusinessType businessType);
}