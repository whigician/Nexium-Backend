﻿using Nexium.API.Entities;

namespace Nexium.API.Data.Repositories;

public interface IContactTypesRepository
{
    public Task<List<ContactType>>
        GetAllContactTypes(CancellationToken cancellationToken, string selectedLanguage);

    public Task<ContactType> GetSingleContactTypeById(byte contactTypeId, CancellationToken cancellationToken,
        string selectedLanguage = "fr",
        bool forView = false);

    Task<ContactType> CreateContactType(ContactType contactType, CancellationToken cancellationToken);
    Task UpdateContactType(ContactType contactType, CancellationToken cancellationToken);
    Task DeleteContactType(ContactType existingContactType, CancellationToken cancellationToken);

    Task<List<ContactTypeTranslation>> GetASingleContactTypeTranslations(byte contactTypeId,
        CancellationToken cancellationToken, bool forView = false);

    Task AddTranslations(List<ContactTypeTranslation> translationsToCreate, CancellationToken cancellationToken);
    Task UpdateTranslations(List<ContactTypeTranslation> translationsToUpdate, CancellationToken cancellationToken);
    Task RemoveTranslations(List<ContactTypeTranslation> translationsToDelete, CancellationToken cancellationToken);
}