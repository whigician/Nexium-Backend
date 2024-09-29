using Nexium.API.Entities;
using Nexium.API.TransferObjects.Translation;
using Riok.Mapperly.Abstractions;

namespace Nexium.API.Configuration;

[Mapper]
public partial class TranslationMapper
{
    public partial List<TranslationView> MapToBusinessTypeTranslationViewList(
        List<BusinessTypeTranslation> businessTypeViewList);

    public partial List<BusinessTypeTranslation> MapToBusinessTypeTranslationList(
        List<TranslationSave> businessTypeTranslationViewList);

    public partial List<TranslationView> MapToIndustryTranslationViewList(
        List<IndustryTranslation> businessTypeViewList);

    public partial List<IndustryTranslation> MapToIndustryTranslationList(
        List<TranslationSave> industryViewList);

    public partial List<TranslationView> MapToBusinessStatusTranslationViewList(
        List<BusinessStatusTranslation> businessStatusViewList);

    public partial List<BusinessStatusTranslation> MapToBusinessStatusTranslationList(
        List<TranslationSave> businessStatusTranslationViewList);

    public partial List<TranslationView> MapToTargetMarketTranslationViewList(
        List<TargetMarketTranslation> targetMarketViewList);

    public partial List<TargetMarketTranslation> MapToTargetMarketTranslationList(
        List<TranslationSave> targetMarketTranslationViewList);

    public partial List<TranslationView> MapToCurrencyTranslationViewList(
        List<CurrencyTranslation> targetMarketViewList);

    public partial List<CurrencyTranslation> MapToCurrencyTranslationList(
        List<TranslationSave> currencyTranslationViewList);

    public partial List<TranslationView> MapToAddressTypeTranslationViewList(
        List<AddressTypeTranslation> addressTypeViewList);

    public partial List<AddressTypeTranslation> MapToAddressTypeTranslationList(
        List<TranslationSave> addressTypeTranslationViewList);

    public partial List<TranslationView> MapToContactTypeTranslationViewList(
        List<ContactTypeTranslation> contactTypeViewList);

    public partial List<ContactTypeTranslation> MapToContactTypeTranslationList(
        List<TranslationSave> contactTypeTranslationViewList);

    public partial List<TranslationView> MapToIdentifierTypeTranslationViewList(
        List<IdentifierTypeTranslation> identifierTypeViewList);

    public partial List<IdentifierTypeTranslation> MapToIdentifierTypeTranslationList(
        List<TranslationSave> identifierTypeTranslationViewList);
}