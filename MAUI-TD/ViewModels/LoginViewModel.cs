using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI_TD.Services;

namespace MAUI_TD.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private readonly MorpionApiService _api;

    [ObservableProperty] string email = "";
    [ObservableProperty] string password = "";
    [ObservableProperty] string? errorMessage;

    public LoginViewModel(MorpionApiService api)
    {
        _api = api;
    }

    [RelayCommand]
    async Task Login()
    {
        ErrorMessage = null;
        var token = await _api.LoginAsync(Email, Password);

        if (token == null)
        {
            ErrorMessage = "Email ou mot de passe incorrect";
            return;
        }

        _api.SetToken(token);
        await Shell.Current.GoToAsync("//MainPage");
    }

    [RelayCommand]
    async Task Register()
    {
        ErrorMessage = null;
        var result = await _api.RegisterAsync(Email, Password);

        if (result == null)
        {
            ErrorMessage = "Erreur lors de l'inscription";
            return;
        }

        // Auto-login after register
        await Login();
    }
}