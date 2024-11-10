using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;
using Nexium.API.Configuration;
using Nexium.API.TransferObjects;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Nexium.API.Services.Implementation;

public class FileStorageService(IOptions<FileStorageSettings> settings)
    : IFileStorageService
{
    private readonly FileStorageSettings _settings = settings.Value;

    public async Task<SaveLogoResponse> SaveOrReplaceLogoAsync(IFormFile file, string entityType, string entityId,
        bool resize = false)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("File is null or empty");
        var baseFolderPath =
            Path.Combine(Directory.GetCurrentDirectory(), _settings.UploadDirectory, "Logos", entityType);
        if (!Directory.Exists(baseFolderPath))
            Directory.CreateDirectory(baseFolderPath);
        var fileName = entityId + Path.GetExtension(file.FileName);
        var originalFilePath = Path.Combine(baseFolderPath, "original_" + fileName);
        if (File.Exists(originalFilePath)) File.Delete(originalFilePath);
        await using (var fileStream = new FileStream(originalFilePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        string resizedFilePath = null;
        var isSvg = Path.GetExtension(originalFilePath).Equals(".svg", StringComparison.OrdinalIgnoreCase);
        if (!resize)
            return new SaveLogoResponse
            {
                OriginalLogoPath = originalFilePath.Replace("\\", "/"),
                ResizedLogoPath = null
            };
        if (isSvg)
        {
            resizedFilePath = Path.Combine(baseFolderPath, "resized_" + fileName);
            if (File.Exists(resizedFilePath)) File.Delete(resizedFilePath);
            using var reader = new StreamReader(file.OpenReadStream());
            var svgContent = await reader.ReadToEndAsync();
            svgContent = ResizeSvgContent(svgContent, 300, 300);

            await File.WriteAllTextAsync(resizedFilePath, svgContent);
        }
        else
        {
            resizedFilePath = Path.Combine(baseFolderPath, "resized_" + fileName);
            if (File.Exists(resizedFilePath)) File.Delete(resizedFilePath);

            using var image = await Image.LoadAsync(file.OpenReadStream());
            image.Mutate(x => x.Resize(new ResizeOptions
            {
                Mode = ResizeMode.Max,
                Size = new Size(300, 300)
            }));
            await image.SaveAsync(resizedFilePath);
        }

        return new SaveLogoResponse
        {
            OriginalLogoPath = originalFilePath.Replace("\\", "/"),
            ResizedLogoPath = resizedFilePath.Replace("\\", "/")
        };
    }

    private static string ResizeSvgContent(string svgContent, int width, int height)
    {
        svgContent = Regex.Replace(svgContent, "width=\"(.*?)\"", $"width=\"{width}\"");
        svgContent = Regex.Replace(svgContent, "height=\"(.*?)\"", $"height=\"{height}\"");

        return svgContent;
    }
}