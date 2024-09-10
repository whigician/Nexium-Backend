using Nexium.API.Entities;
using Nexium.API.TransferObjects.IndustryTranslation;
using Riok.Mapperly.Abstractions;

namespace Nexium.API.Configuration;

[Mapper]
public partial class IndustryTranslationMapper
{
    public partial List<IndustryTranslationView> MapToIndustryTranslationViewList(
        List<IndustryTranslation> industryViewList);

    public partial List<IndustryTranslation> MapToIndustryTranslationList(
        List<IndustryTranslationSave> industryTranslationViewList);
}