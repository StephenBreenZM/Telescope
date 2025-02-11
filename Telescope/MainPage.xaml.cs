using System.Diagnostics;
using System.Globalization;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Media;

namespace Telescope;

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

    // Example usage in a button click event handler:
    private void OnMapButtonClicked(object sender, EventArgs e)
    {
        OpenMapToLocation(52.35790432271456, -7.74245839859923, "Zeromission HQ"); // Opens to the Space Needle
    }
}