namespace Nexium.API.Exceptions;

public class EntityNotFoundException(string entityName, string propertyName, string propertyValue)
    : Exception($"{entityName} with {propertyName} = {propertyValue} not found.");