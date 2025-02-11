using Microsoft.Identity.Client;
using Telescope.Helpers;

namespace Telescope.Services;

public partial class Authenticator
{
    public async Task<AuthenticationResult> Authenticate(string authority, string clientId, 
        IEnumerable<string> scopes, string? returnUri = null)
    {
        AuthenticationResult authenticationResult;
        object? parentWindow;
        var app = PublicClientApplicationBuilder.Create(clientId)
            .WithAuthority(authority)
            .WithRedirectUri(returnUri ?? Constants.RedirectUrl)
            .Build();
        
#if ANDROID
		parentWindow = Platform.CurrentActivity;
#else		
        parentWindow = await MainThread.InvokeOnMainThreadAsync(Platform.GetCurrentUIViewController).ConfigureAwait(false);
#endif
        authenticationResult = await app.AcquireTokenInteractive(scopes).WithParentActivityOrWindow(parentWindow).ExecuteAsync();
        
        return authenticationResult;
//         
// #if IOS
//         var currentViewController = await MainThread.InvokeOnMainThreadAsync(Platform.GetCurrentUIViewController);
//         authenticationResult = await app.AcquireTokenInteractive(scopes)
//             .WithParentActivityOrWindow(currentViewController)
//             .ExecuteAsync();
//         return authenticationResult;
// #endif
//         authenticationResult = await app.AcquireTokenInteractive(scopes)
//             .ExecuteAsync();
//         return authenticationResult;
    }
}