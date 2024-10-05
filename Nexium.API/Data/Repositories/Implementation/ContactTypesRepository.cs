using Microsoft.EntityFrameworkCore;
using Nexium.API.Entities;

namespace Nexium.API.Data.Repositories.Implementation;

public class ContactTypesRepository(NexiumDbContext dbContext)
    : IContactTypesRepository
{
    public Task<List<ContactType>> GetAllContactTypes(CancellationToken cancellationToken,
        string selectedLanguage = "fr")
    {
        return dbContext.ContactTypes
            .Include(i => i.Translations.Where(t => t.LanguageCode == selectedLanguage))
            .AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<ContactType> GetSingleContactTypeById(byte contactTypeId, CancellationToken cancellationToken,
        string selectedLanguage, bool forView = false)
    {
        var fetchQuery =
            dbContext.ContactTypes.Include(x => x.Translations.Where(t => t.LanguageCode == selectedLanguage));
        if (forView)
            return await fetchQuery.AsNoTracking().FirstOrDefaultAsync(x => x.Id == contactTypeId, cancellationToken);
        return await fetchQuery.FirstOrDefaultAsync(x => x.Id == contactTypeId, cancellationToken);
    }

    public async Task<ContactType> CreateContactType(ContactType contactType, CancellationToken cancellationToken)
    {
        dbContext.ContactTypes.Add(contactType);
        await dbContext.SaveChangesAsync(cancellationToken);
        return contactType;
    }

    public async Task UpdateContactType(ContactType contactType, CancellationToken cancellationToken)
    {
        dbContext.ContactTypes.Update(contactType);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteContactType(ContactType existingContactType, CancellationToken cancellationToken)
    {
        dbContext.ContactTypes.Remove(existingContactType);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<List<ContactTypeTranslation>> GetASingleContactTypeTranslations(byte contactTypeId,
        CancellationToken cancellationToken, bool forView = false)
    {
        var fetchQuery = dbContext.ContactTypesTranslations
            .Where(x => x.ContactTypeId == contactTypeId);
        return forView
            ? fetchQuery.AsNoTracking().ToListAsync(cancellationToken)
            : fetchQuery.ToListAsync(cancellationToken);
    }

    public async Task AddTranslations(List<ContactTypeTranslation> translationsToCreate,
        CancellationToken cancellationToken)
    {
        await dbContext.ContactTypesTranslations.AddRangeAsync(translationsToCreate, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateTranslations(List<ContactTypeTranslation> translationsToUpdate,
        CancellationToken cancellationToken)
    {
        dbContext.UpdateRange(translationsToUpdate);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveTranslations(List<ContactTypeTranslation> translationsToDelete,
        CancellationToken cancellationToken)
    {
        dbContext.RemoveRange(translationsToDelete);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}