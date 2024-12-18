﻿using Nexium.API.TransferObjects.ContactType;

namespace Nexium.API.Services;

public interface IContactTypesService
{
    public Task<List<ContactTypeView>> GetAllContactTypes(CancellationToken cancellationToken);
    public Task<ContactTypeView> GetSingleContactTypeById(byte contactTypeId, CancellationToken cancellationToken);

    public Task<ContactTypeView> CreateContactType(ContactTypeSave contactTypeToCreate,
        CancellationToken cancellationToken);

    public Task UpdateContactType(byte contactTypeId, ContactTypeSave contactTypeToUpdate,
        CancellationToken cancellationToken);

    public Task DeleteContactType(byte contactTypeId, CancellationToken cancellationToken);
}