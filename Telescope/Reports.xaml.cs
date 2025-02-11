using System.Diagnostics;
using Telescope.ViewModels;

namespace Telescope;

public partial class Reports : ContentPage
{
    ReportsViewModel vm;
    public Reports(ReportsViewModel vm)
    {
        InitializeComponent();
        this.vm = vm;
        this.BindingContext = vm;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (vm != null)
        {
            Debug.WriteLine("VM is not null");
            this.vm.SelectedReport = null;
            this.vm.IsReportsListRefreshing = true;
        }
    }
    // protected override void OnAppearing()
    // {
    //     base.OnAppearing();
    //     LoadWebView("https://app.powerbi.com/links/UZN_3KBQ0U?ctid=98d6686e-0da4-43d4-9d28-1f70cddd3861&pbi_source=linkShare");
    // }
    // private void LoadWebView(string originalUrl)
    // {
    //     try
    //     {
    //         DisplayAlert("Loading", "Loading web page...", "OK");
    //         var encodedUrl = Uri.EscapeDataString(originalUrl);
    //         ReportWebView.Source = encodedUrl; // Or MyWebView.Source = uri;
    //     }
    //     catch (UriFormatException ex)
    //     {
    //         // Handle invalid URL format
    //         Debug.WriteLine($"Invalid URL: {ex.Message}");
    //         DisplayAlert("Error", "Invalid URL format.", "OK");
    //     }
    //     catch (Exception ex)
    //     {
    //         // Handle other exceptions
    //         Debug.WriteLine($"Error loading WebView: {ex.Message}");
    //         DisplayAlert("Error", "Error loading web page.", "OK");
    //     }
    // }
    // private void ReportWebView_Navigating(object? sender, EventArgs e)
    // {
    //     var encodedUrl = Uri.EscapeDataString(); // Or use new Uri(url)
    //     ReportWebView.Source = encodedUrl;
    // }
}