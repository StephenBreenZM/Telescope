using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Media;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices.Sensors;
using Telescope.MSALClient;

namespace Telescope.Views;

public partial class MainPage
{

    private readonly ISpeechToText _speechToText;

    public MainPage()
    {
        InitializeComponent();
        _speechToText = new SpeechToTextImplementation(); // Replace with your actual implementation
    } 
    
    async void StartListening(object? sender, EventArgs eventArgs)
    {
        var cancellationToken = CancellationToken.None;
        var isGranted = await _speechToText.RequestPermissions(cancellationToken);
        if (!isGranted)
        {
            await Toast.Make("Permission not granted").Show(CancellationToken.None);
            return;
        }

        _speechToText.RecognitionResultUpdated += OnRecognitionTextUpdated;
        _speechToText.RecognitionResultCompleted += OnRecognitionTextCompleted;
        await _speechToText.StartListenAsync(new SpeechToTextOptions	{ Culture = CultureInfo.CurrentCulture, ShouldReportPartialResults = true }, CancellationToken.None);
    }

    async void StopListening(object? sender, EventArgs eventArgs)
    {
        await _speechToText.StopListenAsync(CancellationToken.None);
        _speechToText.RecognitionResultUpdated -= OnRecognitionTextUpdated;
        _speechToText.RecognitionResultCompleted -= OnRecognitionTextCompleted;
    }

    private void OnRecognitionTextUpdated(object? sender, SpeechToTextRecognitionResultUpdatedEventArgs args)
    {
    }

    private void OnRecognitionTextCompleted(object? sender, SpeechToTextRecognitionResultCompletedEventArgs args)
    {
    }
    
    public static async void OpenMapToLocation(double latitude, double longitude, string? name = null)
    {
        try
        {
            var location = new Location(latitude, longitude);
            var options = new MapLaunchOptions { Name = name }; 

            await Map.OpenAsync(location, options);
        }
        catch (Exception ex)
        {
            // Handle exceptions, such as the map app not being installed
            Debug.WriteLine($"Error opening map: {ex.Message}");
        }
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
        // await DisplayAlert("Auth0 Sign In", "Sign in with Auth0 not yet implemented", "OK");
        try
        {
            
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            await DisplayAlert("Error", $"{exception.Message}", "OK");
        }
    }
    
    // Example usage in a button click event handler:
    private void OnMapButtonClicked(object sender, EventArgs e)
    {
        OpenMapToLocation(52.35790432271456, -7.74245839859923, "Zeromission HQ"); // Opens to the Space Needle
    }
}