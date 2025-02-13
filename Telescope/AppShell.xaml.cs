using Microsoft.Maui.Controls;
using Telescope.Helpers;
using Telescope.Views;

namespace Telescope;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("camerafeatures", typeof(CameraFeatures));
        Routing.RegisterRoute("simplefeatures", typeof(SimpleFeatures));
        Routing.RegisterRoute(Constants.DetailsRoute, typeof(WebViewPage));
    }
}