using Microsoft.EntityFrameworkCore;
using Nexium.API.Entities;

namespace Nexium.API.Data.Repositories.Implementation;

public class AddressTypesRepository(NexiumDbContext dbContext)
    : IAddressTypesRepository
{
    public Task<List<AddressType>> GetAllAddressTypes(CancellationToken cancellationToken)
    {
        return dbContext.AddressTypes
            .AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<AddressType> GetSingleAddressTypeById(byte addressTypeId, CancellationToken cancellationToken,
        bool forView = false)
    {
        if (forView)
            return await dbContext.AddressTypes.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == addressTypeId, cancellationToken);
        return await dbContext.AddressTypes.FirstOrDefaultAsync(x => x.Id == addressTypeId, cancellationToken);
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
}