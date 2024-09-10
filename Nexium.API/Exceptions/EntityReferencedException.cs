namespace Nexium.API.Exceptions;

public class EntityReferencedException(string entityName, string propertyName, string propertyValue)
    : Exception($"{entityName} with {propertyName} = {propertyValue} is referenced by other entities");