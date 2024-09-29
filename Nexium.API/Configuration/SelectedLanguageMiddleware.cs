namespace Nexium.API.Configuration;

public class SelectedLanguageMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var userSelectedLanguage = context.Request.Headers["X-Language"].ToString();
        if (string.IsNullOrEmpty(userSelectedLanguage))
            userSelectedLanguage = context.Request.Headers["Accept-Language"].ToString().Split(',').FirstOrDefault()
                ?.Trim();
        if (string.IsNullOrEmpty(userSelectedLanguage)) userSelectedLanguage = "fr-FR";
        context.Items["SelectedLanguage"] = userSelectedLanguage;
        await next(context);
    }
}