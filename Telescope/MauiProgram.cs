using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Telescope.Services;
using ZXing.Net.Maui.Controls;

namespace Telescope;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseBarcodeReader()
            .UseMauiCommunityToolkit()
            //.UseLocalNotification()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif
        builder.Services.AddTransient<ApiService>();
        
        // builder.Services.AddSingleton<IPowerBIService, PowerBIService>();
        //
        // builder.Services.AddTransient<CameraFeatures>();
        // builder.Services.AddTransient<SimpleFeatures>();
        // builder.Services.AddTransient<ReportsPage>();
        // builder.Services.AddTransient<ReportsViewModel>();
        // builder.Services.AddTransient<WebViewModel>();
        // builder.Services.AddTransient<WebViewPage>();

        
        return builder.Build();
    }
}