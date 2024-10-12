namespace Nexium.API.Services.Implementation;

public class SelectedLanguageService(IHttpContextAccessor httpContextAccessor)
{
    public string GetSelectedLanguage()
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext?.Items["SelectedLanguage"] == null)
            return "fr";
        var selectedLanguage = httpContext.Items["SelectedLanguage"]?.ToString();
        return string.IsNullOrEmpty(selectedLanguage) ? "fr" : selectedLanguage;
    }
}