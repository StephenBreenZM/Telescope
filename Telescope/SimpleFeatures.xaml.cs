using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Font = Microsoft.Maui.Font;

namespace Telescope;

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
        var cancellationTokenSource = new CancellationTokenSource();

        var snackbarOptions = new SnackbarOptions
        {
            BackgroundColor = Colors.Red,
            TextColor = Colors.Green,
            ActionButtonTextColor = Colors.Yellow,
            CornerRadius = new CornerRadius(10),
            Font = Font.SystemFontOfSize(14),
            ActionButtonFont = Font.SystemFontOfSize(14),
            CharacterSpacing = 0.5
        };

        var text = "This is a Snackbar";
        var actionButtonText = "Click Here to Dismiss";
        async void Action() => await DisplayAlert("Snackbar ActionButton Tapped", "The user has tapped the Snackbar ActionButton", "OK");
        var duration = TimeSpan.FromSeconds(3);

        var snackbar = Snackbar.Make(text, Action, actionButtonText, duration, snackbarOptions);
        await snackbar.Show(cancellationTokenSource.Token);
        // try
        // {
        //     var notification = new Toast
        //     {
        //         Text = "This is a test notification.",
        //         Duration = ToastDuration.Short
        //     };
        //
        //     await notification.Show();
        // }
        // catch (Exception ex)
        // {
        //     // Handle any exceptions
        //     await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        // }
    }

}