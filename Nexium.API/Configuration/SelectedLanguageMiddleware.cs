namespace Nexium.API.Configuration;

public class SelectedLanguageMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var LanguageOverride = context.Request.Headers["X-Language-Override"].ToString();
        var userSelectedLanguage = !string.IsNullOrEmpty(LanguageOverride) ? LanguageOverride : context.Request.Headers["X-Language"].ToString();
        if (string.IsNullOrEmpty(userSelectedLanguage))
            userSelectedLanguage = context.Request.Headers["Accept-Language"].ToString().Split(',').FirstOrDefault()
                ?.Trim();
        if (string.IsNullOrEmpty(userSelectedLanguage)) userSelectedLanguage = "fr-FR";
        context.Items["SelectedLanguage"] = userSelectedLanguage;
        await next(context);
    }
}