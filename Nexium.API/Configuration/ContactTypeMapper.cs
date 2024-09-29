using Nexium.API.Entities;
using Nexium.API.TransferObjects.ContactType;
using Riok.Mapperly.Abstractions;

namespace Nexium.API.Configuration;

[Mapper]
public partial class ContactTypeMapper
{
    public partial ContactType MapToContactType(ContactTypeSave contactTypeSave);
    public partial ContactTypeView MapToContactTypeView(ContactType contactType);
}