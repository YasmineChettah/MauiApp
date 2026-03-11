using CommunityToolkit.Mvvm.Messaging;
using MAUI_TD.ViewModels;

namespace MAUI_TD.Views;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;

    WeakReferenceMessenger.Default.Register<string>(this, async (r, player) =>
        {
            await DisplayAlertAsync("Winner", $"{player}", "OK");
        });
    }
}