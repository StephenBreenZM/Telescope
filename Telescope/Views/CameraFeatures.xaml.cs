using System;
using System.IO;
using System.Linq;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Dispatching;
using Microsoft.Maui.Media;
using ZXing.Net.Maui;

namespace Telescope.Views;

public partial class CameraFeatures
{
    public CameraFeatures()
    {
        InitializeComponent();
        // CameraBarcodeReaderView.Options = new BarcodeReaderOptions
        // {
        //     Formats = BarcodeFormat.QrCode,
        //     AutoRotate = true,
        //     Multiple = false
        // };
    }

    private void OnScanQrCodeClicked(object? sender, EventArgs eventArgs)
    {
        // CameraBarcodeReaderView.IsEnabled = true;
        // CameraBarcodeReaderView.IsVisible = true;
    }
    
    private async void OnTakePhotoClicked(object sender, EventArgs e)
    {
        // try
        // {
        //     if (MediaPicker.Default.IsCaptureSupported)
        //     {
        //         try
        //         {
        //             var photo = await MediaPicker.Default.CapturePhotoAsync();
        //
        //             if (photo == null) return;
        //             // Load the image into an Image control
        //             await using var stream = await photo.OpenReadAsync();
        //             using var memoryStream = new MemoryStream();
        //             await stream.CopyToAsync(memoryStream);
        //             PhotoImage.Source = ImageSource.FromStream(() => new MemoryStream(memoryStream.ToArray()));
        //         }
        //         catch (FeatureNotSupportedException)
        //         {
        //             // Handle the case where the device doesn't support capturing photos
        //             await DisplayAlert("Error", "This device does not support capturing photos.", "OK");
        //         }
        //         catch (PermissionException)
        //         {
        //             // Handle the case where the user has not granted permission to access the camera
        //             await DisplayAlert("Error", "Camera permission not granted.", "OK");
        //         }
        //         catch (Exception ex)
        //         {
        //             // Handle other exceptions
        //             await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        //         }
        //     }
        //     else
        //     {
        //         await DisplayAlert("Error", "Camera capture is not supported on this device.", "OK");
        //     }
        // }
        // catch (Exception exception)
        // {
        //     await DisplayAlert("Error", $"Unexpected Error found: {exception.Message}", "OK");
        // }
    }

    private void CameraBarcodeReaderView_OnBarcodesDetected(object? sender, BarcodeDetectionEventArgs e)
    {
        var firstCode = e.Results.FirstOrDefault();
        if (firstCode is null) return;

        Dispatcher.DispatchAsync(async () =>
        {
            await DisplayAlert("Barcode Detected", firstCode.Value, "OK");
            // CameraBarcodeReaderView.IsEnabled = false;
            // CameraBarcodeReaderView.IsVisible = false;
        });
    }
}