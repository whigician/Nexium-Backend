﻿using Nexium.API.Entities;

namespace Nexium.API.Data.Repositories;

public interface IIdentifierTypesRepository
{
    public Task<List<IdentifierType>>
        GetAllIdentifierTypes(CancellationToken cancellationToken, string selectedLanguage);

    public Task<IdentifierType> GetSingleIdentifierTypeById(byte identifierTypeId, CancellationToken cancellationToken,
        string selectedLanguage = "fr",
        bool forView = false);

    Task<IdentifierType> CreateIdentifierType(IdentifierType identifierType, CancellationToken cancellationToken);
    Task UpdateIdentifierType(IdentifierType identifierType, CancellationToken cancellationToken);
    Task DeleteIdentifierType(IdentifierType existingIdentifierType, CancellationToken cancellationToken);
}