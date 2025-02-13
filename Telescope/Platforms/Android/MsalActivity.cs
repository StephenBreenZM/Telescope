using Android.App;
using Android.Content;
using Microsoft.Identity.Client;

namespace Telescope;

[Activity(Exported =true)]
[IntentFilter(new[] { Intent.ActionView },
    Categories = new[] { Intent.CategoryBrowsable, Intent.CategoryDefault },
    DataHost = "auth",
    DataScheme = "ad76271a-f92e-45a0-89ac-619c34d13273")]
public class MsalActivity : MauiAppCompatActivity
{
}