using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace Telescope.Views;

public partial class SimpleFeatures : ContentPage
{
    private int _count;
    
    public SimpleFeatures()
    {
        InitializeComponent();
    }
    
    private void OnCounterClicked(object sender, EventArgs e)
    {
        _count++;

        CounterBtn.Text = _count == 1 ? $"Clicked {_count} time" : $"Clicked {_count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }
    
    private async void OnNotifyButtonClicked(object sender, EventArgs e)
    {
        try
        {
            var notification = new Toast
            {
                Text = "This is a test notification.",
                Duration = ToastDuration.Short
            };
        
            await notification.Show();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }
    
    private async void OnMapButtonClicked(object sender, EventArgs e)
    {
        try
        {
            await Map.OpenAsync(52.35790432271456, -7.74245839859923, new MapLaunchOptions { Name = "Zeromission HQ" });
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }
}