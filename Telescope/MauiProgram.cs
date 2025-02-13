using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using Telescope.Services;
using Telescope.ViewModels;
using Telescope.Views;
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
        
        // builder.Services.AddSingleton<IPowerBIService, PowerBIService>();
        //
        // builder.Services.AddTransient<CameraFeatures>();
        // builder.Services.AddTransient<SimpleFeatures>();
        // builder.Services.AddTransient<ReportsPage>();
        // builder.Services.AddTransient<ReportsViewModel>();
        // builder.Services.AddTransient<WebViewModel>();
        // builder.Services.AddTransient<WebViewPage>();
        
        builder.Logging.AddDebug();

        return builder.Build();
    }
}