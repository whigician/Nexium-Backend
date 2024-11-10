using Nexium.API.TransferObjects;

namespace Nexium.API.Services;

public interface IFileStorageService
{
    Task<SaveLogoResponse> SaveOrReplaceLogoAsync(IFormFile file, string entityType, string fileName,
        bool resize = false);
}