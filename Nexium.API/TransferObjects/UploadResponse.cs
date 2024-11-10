namespace Nexium.API.TransferObjects;

public class UploadResponse
{
    public bool Success { get; set; }
    public string FileName { get; set; }
    public string OriginalFilePath { get; set; }
    public string ResizedFilePath { get; set; }
}