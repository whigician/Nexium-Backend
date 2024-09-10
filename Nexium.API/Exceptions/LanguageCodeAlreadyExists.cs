namespace Nexium.API.Exceptions;

public class LanguageCodeAlreadyExists(string languageCode, string entityName)
    : Exception($"Language code {languageCode} already exists for {entityName}.");