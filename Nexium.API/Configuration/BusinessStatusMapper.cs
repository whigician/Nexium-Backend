using Nexium.API.Entities;
using Nexium.API.TransferObjects.BusinessStatus;
using Riok.Mapperly.Abstractions;

namespace Nexium.API.Configuration;

[Mapper]
public partial class BusinessStatusMapper
{
    public partial BusinessStatus MapToBusinessStatus(BusinessStatusSave businessStatusSave);
    public partial BusinessStatusView MapToBusinessStatusView(BusinessStatus businessType);
}