using System.ComponentModel.DataAnnotations;

namespace Nexium.API.Validators;

public class FileValidationAttribute(long maxSize, params string[] allowedExtensions) : ValidationAttribute
{
    private long MaxSize { get; } = maxSize;
    private string[] AllowedExtensions { get; } = allowedExtensions;

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is not IFormFile file) return new ValidationResult("Invalid file.");
        if (file.Length > MaxSize)
            return new ValidationResult(
                $"The file size exceeds the maximum allowed size of {MaxSize / (1024 * 1024)} MB.");

        var fileExtension = Path.GetExtension(file.FileName).ToLower();
        return Array.Exists(AllowedExtensions, ext => ext.Equals(fileExtension))
            ? ValidationResult.Success
            : new ValidationResult(
                $"Only the following file types are allowed: {string.Join(", ", AllowedExtensions)}.");
    }
}