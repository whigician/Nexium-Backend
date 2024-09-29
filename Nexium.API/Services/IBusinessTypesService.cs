using Nexium.API.TransferObjects.BusinessType;
using Nexium.API.TransferObjects.Translation;

namespace Nexium.API.Services;

public interface IBusinessTypesService
{
    public Task<List<BusinessTypeView>> GetAllBusinessTypes(CancellationToken cancellationToken);
    public Task<BusinessTypeView> GetSingleBusinessTypeById(byte businessTypeId, CancellationToken cancellationToken);

    public Task<BusinessTypeView> CreateBusinessType(BusinessTypeSave businessTypeToCreate,
        CancellationToken cancellationToken);

    public Task UpdateBusinessType(byte businessTypeId, BusinessTypeSave businessTypeToUpdate,
        CancellationToken cancellationToken);

    public Task DeleteBusinessType(byte businessTypeId, CancellationToken cancellationToken);

    Task<List<TranslationView>> GetASingleBusinessTypeTranslations(byte businessTypeId,
        CancellationToken cancellationToken);

    Task UpdateASingleBusinessTypeTranslations(byte businessTypeId,
        List<TranslationSave> businessTypeTranslationsToSave,
        CancellationToken cancellationToken);
}