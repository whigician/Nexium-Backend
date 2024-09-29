using Nexium.API.Entities;
using Nexium.API.TransferObjects.AddressType;
using Riok.Mapperly.Abstractions;

namespace Nexium.API.Configuration;

[Mapper]
public partial class AddressTypeMapper
{
    public partial AddressType MapToAddressType(AddressTypeSave addressTypeSave);
    public partial AddressTypeView MapToAddressTypeView(AddressType addressType);
}