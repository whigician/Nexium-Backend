using Nexium.API.Entities;
using Nexium.API.TransferObjects.PersonIdentifierType;
using Riok.Mapperly.Abstractions;

namespace Nexium.API.Configuration;

[Mapper]
public partial class PersonIdentifierTypeMapper
{
    public partial PersonIdentifierType MapToPersonIdentifierType(PersonIdentifierTypeSave personIdentifierTypeSave);
    public partial PersonIdentifierTypeView MapToPersonIdentifierTypeView(PersonIdentifierType personIdentifierType);
}