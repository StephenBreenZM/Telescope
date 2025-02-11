using CommunityToolkit.Mvvm.ComponentModel;
using Telescope.Helpers;

namespace Telescope.ViewModels;

[QueryProperty(nameof(WebPageUrl), Constants.ReportUrl)]
public partial class WebViewModel : BaseViewModel
{
    [ObservableProperty]
    string webPageUrl;
}