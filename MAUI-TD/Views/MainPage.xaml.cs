using CommunityToolkit.Mvvm.Messaging;
using MAUI_TD.ViewModels;

namespace MAUI_TD.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new MainViewModel();

        WeakReferenceMessenger.Default.Register<string>(this, async (r, player) =>
        {
            await DisplayAlertAsync("Winner", $"Player {player} wins", "OK");
        });
    }
}