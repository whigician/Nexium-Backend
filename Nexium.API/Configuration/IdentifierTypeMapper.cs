using Nexium.API.Entities;
using Nexium.API.TransferObjects.IdentifierType;
using Riok.Mapperly.Abstractions;

namespace Nexium.API.Configuration;

[Mapper]
public partial class IdentifierTypeMapper
{
    public partial IdentifierType MapToIdentifierType(IdentifierTypeSave identifierTypeSave);
    public partial IdentifierTypeView MapToIdentifierTypeView(IdentifierType identifierType);
}