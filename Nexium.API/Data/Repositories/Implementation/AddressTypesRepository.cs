using Microsoft.EntityFrameworkCore;
using Nexium.API.Entities;

namespace Nexium.API.Data.Repositories.Implementation;

public class AddressTypesRepository(NexiumDbContext dbContext)
    : IAddressTypesRepository
{
    public Task<List<AddressType>> GetAllAddressTypes(CancellationToken cancellationToken,
        string selectedLanguage = "fr-FR")
    {
        return dbContext.AddressTypes
            .Include(i => i.Translations.Where(t => t.LanguageCode == selectedLanguage))
            .AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<AddressType> GetSingleAddressTypeById(byte addressTypeId, CancellationToken cancellationToken,
        string selectedLanguage, bool forView = false)
    {
        var fetchQuery =
            dbContext.AddressTypes.Include(x => x.Translations.Where(t => t.LanguageCode == selectedLanguage));
        if (forView)
            return await fetchQuery.AsNoTracking().FirstOrDefaultAsync(x => x.Id == addressTypeId, cancellationToken);
        return await fetchQuery.FirstOrDefaultAsync(x => x.Id == addressTypeId, cancellationToken);
    }

    public async Task<AddressType> CreateAddressType(AddressType addressType, CancellationToken cancellationToken)
    {
        dbContext.AddressTypes.Add(addressType);
        await dbContext.SaveChangesAsync(cancellationToken);
        return addressType;
    }

    public async Task UpdateAddressType(AddressType addressType, CancellationToken cancellationToken)
    {
        dbContext.AddressTypes.Update(addressType);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAddressType(AddressType existingAddressType, CancellationToken cancellationToken)
    {
        dbContext.AddressTypes.Remove(existingAddressType);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<List<AddressTypeTranslation>> GetASingleAddressTypeTranslations(byte addressTypeId,
        CancellationToken cancellationToken, bool forView = false)
    {
        var fetchQuery = dbContext.AddressTypesTranslations
            .Where(x => x.AddressTypeId == addressTypeId);
        return forView
            ? fetchQuery.AsNoTracking().ToListAsync(cancellationToken)
            : fetchQuery.ToListAsync(cancellationToken);
    }

    public async Task AddTranslations(List<AddressTypeTranslation> translationsToCreate,
        CancellationToken cancellationToken)
    {
        await dbContext.AddressTypesTranslations.AddRangeAsync(translationsToCreate, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateTranslations(List<AddressTypeTranslation> translationsToUpdate,
        CancellationToken cancellationToken)
    {
        dbContext.UpdateRange(translationsToUpdate);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveTranslations(List<AddressTypeTranslation> translationsToDelete,
        CancellationToken cancellationToken)
    {
        dbContext.RemoveRange(translationsToDelete);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}