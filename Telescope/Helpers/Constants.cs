namespace Telescope.Helpers;

public class Constants
{
    public const string ApplicationId = "ad76271a-f92e-45a0-89ac-619c34d13273";

    // public const string RedirectUrl = "https://login.microsoftonline.com/common/oauth2/nativeclient";
    public const string RedirectUrl = $"msal{ApplicationId}://auth";

    public const string OAuth2Authority = "https://login.microsoftonline.com/common";

    public static IReadOnlyList<string> Scopes { get; } = new[]
    {
        "User.Read",
        "https://analysis.windows.net/powerbi/api/Report.Read.All"
    };

    public const string DetailsRoute = "WebView";
    public const string ReportUrl = "https://app.powerbi.com/links/-JBcSrLgse?ctid=98d6686e-0da4-43d4-9d28-1f70cddd3861";
}