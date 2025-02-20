using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.Maui.Controls;
using Telescope.Model;
using Telescope.ViewModels;

namespace Telescope.Views;

public partial class ReportsPage : ContentPage
{
    private ObservableCollection<ReportView> _reports;
    
    public ReportsPage()
    {
        InitializeComponent();
        _reports =
        [
            new ReportView() { Name = "Report 1", Url = "https://example.com/report1" },
            new ReportView() { Name = "Report 2", Url = "https://example.com/report2" }
        ];
        ReportPicker.ItemsSource = _reports;
    }
    public ReportsPage(ObservableCollection<ReportView> reports)
    {
        InitializeComponent();
        _reports = reports;
        ReportPicker.ItemsSource = _reports;
    }
    private void OnReportSelected(object sender, EventArgs e)
    {
        if (ReportPicker.SelectedItem is ReportView selectedReport)
        {
            ReportWebView.Source = selectedReport.Url;
        }
    }
    //
    // public ReportsPage()
    // {
    //     InitializeComponent();
    //     this.vm = new ReportsViewModel();
    //     this.BindingContext = vm;
    // }
    //
    // protected override void OnAppearing()
    // {
    //     base.OnAppearing();
    //     if (vm != null)
    //     {
    //         Debug.WriteLine("VM is not null");
    //         this.vm.SelectedReport = null;
    //         this.vm.IsReportsListRefreshing = true;
    //     }
    // }
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