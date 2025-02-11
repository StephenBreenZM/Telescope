using CommunityToolkit.Mvvm.ComponentModel;

namespace Telescope.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    bool isBusy;
}