using System.Collections.ObjectModel;
using Telescope.MSALClient;
using Auth0.OidcClient;
using Telescope.Model;
using Telescope.Services;

namespace Telescope.Views;

public partial class MainPage
{
    private ObservableCollection<ReportView> _reports;

    public MainPage()
    {
        InitializeComponent();
        _reports =
        [
            new ReportView() { Name = "Report 1", Url = "https://example.com/report1" },
            new ReportView() { Name = "Report 2", Url = "https://example.com/report2" }
        ];
        ReportPicker.ItemsSource = _reports;
    } 
    
    private async void OnO365SignInClicked(object sender, EventArgs e)
    {
        try
        {
            await PublicClientSingleton.Instance.AcquireTokenSilentAsync();
            await Shell.Current.GoToAsync("ReportsPage");
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            await DisplayAlert("Error", $"{exception.Message}", "OK");
        }
    }
    
    private async void OnAuth0SignInClicked(object sender, EventArgs e)
    {
        try
        {
            var client = new Auth0Client(new Auth0ClientOptions
            {
                Domain = "zmission-staging.eu.auth0.com",
                ClientId = "lBaAZszGpbZowwQxBkOBtg6uNCjhC0Jw",
                RedirectUri = "myapp://callback",
                PostLogoutRedirectUri = "myapp://callback",
                Scope = "openid profile email"
            });
            var service = new ApiService(client);
            _reports = await service.GetReportsList();
            ReportPicker.ItemsSource = _reports;
            ReportPicker.SelectedIndex = 0;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            await DisplayAlert("Error", $"{exception.Message}", "OK");
        }
    }

    private void OnReportSelected(object sender, EventArgs e)
    {
        if (ReportPicker.SelectedItem is ReportView selectedReport)
        {
            ReportWebView.Source = selectedReport.Url;
        }
    }
}