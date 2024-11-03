using Nexium.API.Entities;
using Nexium.API.TransferObjects.Translation;
using Riok.Mapperly.Abstractions;

namespace Nexium.API.Configuration;

[Mapper]
public partial class TranslationMapper
{
    public partial List<TranslationMappingView> MapToTranslationMappingViewList(
        List<TranslationMapping> translationMappings);

    public partial List<TranslationMapping> MapToTranslationMappingList(
        List<TranslationMappingSave> translationMappingSaves);
}