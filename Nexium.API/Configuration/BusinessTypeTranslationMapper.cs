using Nexium.API.Entities;
using Nexium.API.TransferObjects.BusinessTypeTranslation;
using Riok.Mapperly.Abstractions;

namespace Nexium.API.Configuration;

[Mapper]
public partial class BusinessTypeTranslationMapper
{
    public partial List<BusinessTypeTranslationView> MapToBusinessTypeTranslationViewList(
        List<BusinessTypeTranslation> businessTypeViewList);

    public partial List<BusinessTypeTranslation> MapToBusinessTypeTranslationList(
        List<BusinessTypeTranslationSave> businessTypeTranslationViewList);
}