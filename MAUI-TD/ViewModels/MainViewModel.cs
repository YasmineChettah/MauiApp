using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace MAUI_TD.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    string? b1;

    [ObservableProperty]
    string? b2;

    [ObservableProperty]
    string? b3;

    [ObservableProperty]
    string? b4;

    [ObservableProperty]
    string? b5;

    [ObservableProperty]
    string? b6;

    [ObservableProperty]
    string? b7;

    [ObservableProperty]
    string? b8;

    [ObservableProperty]
    string? b9;

    string currentPlayer = "X";

    [RelayCommand]
    void Play(string cell)
    {
        switch (cell)
        {
            case "1": if (string.IsNullOrEmpty(B1)) B1 = currentPlayer; break;
            case "2": if (string.IsNullOrEmpty(B2)) B2 = currentPlayer; break;
            case "3": if (string.IsNullOrEmpty(B3)) B3 = currentPlayer; break;
            case "4": if (string.IsNullOrEmpty(B4)) B4 = currentPlayer; break;
            case "5": if (string.IsNullOrEmpty(B5)) B5 = currentPlayer; break;
            case "6": if (string.IsNullOrEmpty(B6)) B6 = currentPlayer; break;
            case "7": if (string.IsNullOrEmpty(B7)) B7 = currentPlayer; break;
            case "8": if (string.IsNullOrEmpty(B8)) B8 = currentPlayer; break;
            case "9": if (string.IsNullOrEmpty(B9)) B9 = currentPlayer; break;
        }

        if (CheckWin())
        {
            WeakReferenceMessenger.Default.Send(currentPlayer);
            ResetGame();
            return;
        }

        currentPlayer = currentPlayer == "X" ? "O" : "X";
    }

    bool CheckWin()
    {
        // lines
        if (B1 == B2 && B2 == B3 && !string.IsNullOrEmpty(B1)) return true;
        if (B4 == B5 && B5 == B6 && !string.IsNullOrEmpty(B4)) return true;
        if (B7 == B8 && B8 == B9 && !string.IsNullOrEmpty(B7)) return true;

        // columns
        if (B1 == B4 && B4 == B7 && !string.IsNullOrEmpty(B1)) return true;
        if (B2 == B5 && B5 == B8 && !string.IsNullOrEmpty(B2)) return true;
        if (B3 == B6 && B6 == B9 && !string.IsNullOrEmpty(B3)) return true;

        // diago
        if (B1 == B5 && B5 == B9 && !string.IsNullOrEmpty(B1)) return true;
        if (B3 == B5 && B5 == B7 && !string.IsNullOrEmpty(B3)) return true;

        return false;
    }

    void ResetGame()
    {
        B1 = B2 = B3 = B4 = B5 = B6 = B7 = B8 = B9 = "";
    }
}