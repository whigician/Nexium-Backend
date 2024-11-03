namespace Nexium.API.Configuration;

public class SelectedLanguageMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var languageOverride = context.Request.Headers["X-Language-Override"].ToString();
        var userSelectedLanguage = !string.IsNullOrEmpty(languageOverride)
            ? languageOverride
            : context.Request.Headers["X-Language"].ToString();
        if (string.IsNullOrEmpty(userSelectedLanguage))
            userSelectedLanguage = context.Request.Headers["Accept-Language"].ToString().Split(',').FirstOrDefault()
                ?.Trim();
        if (string.IsNullOrEmpty(userSelectedLanguage)) userSelectedLanguage = "fr";
        context.Items["SelectedLanguage"] = userSelectedLanguage.ToLower();
        await next(context);
    }
}