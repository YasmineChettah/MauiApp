using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MAUI_TD.Models;
using MAUI_TD.Services;
using System.Collections.ObjectModel;

namespace MAUI_TD.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty] string? b1;
    [ObservableProperty] string? b2;
    [ObservableProperty] string? b3;
    [ObservableProperty] string? b4;
    [ObservableProperty] string? b5;
    [ObservableProperty] string? b6;
    [ObservableProperty] string? b7;
    [ObservableProperty] string? b8;
    [ObservableProperty] string? b9;

    public enum GameMode
    {
        HumanVsHuman,
        HumanVsBot
    }

    public List<GameMode> GameModes { get; } =
    [
        GameMode.HumanVsHuman,
        GameMode.HumanVsBot
    ];

    [ObservableProperty]
    GameMode selectedMode = GameMode.HumanVsHuman;

    string currentPlayer = "X";

    Random random = new Random();

    public ObservableCollection<GameResult> Games { get; } = new();

    MorpionDatabase database;

    public MainViewModel(MorpionDatabase database)
    {
        this.database = database;

        LoadHistory();
    }

    async void LoadHistory()
    {
        var games = await database.GetHistoryAsync();

        foreach (var g in games)
            Games.Add(g);
    }

    [RelayCommand]
    async void Play(string cell)
    {
        if (!PlayCell(cell))
            return;

        if (CheckWin())
        {
            await database.AddResultAsync($"Player {currentPlayer} wins");

            Games.Add(new GameResult
            {
                Result = $"Player {currentPlayer} wins"
            });

            WeakReferenceMessenger.Default.Send($"Player {currentPlayer} wins");

            ResetGame();
            return;
        }

        if (IsBoardFull())
        {
            await database.AddResultAsync("Draw");

            Games.Add(new GameResult
            {
                Result = "Draw"
            });

            WeakReferenceMessenger.Default.Send("Draw");

            ResetGame();
            return;
        }

        if (SelectedMode == GameMode.HumanVsBot)
        {
            BotPlay();
        }
        else
        {
            currentPlayer = currentPlayer == "X" ? "O" : "X";
        }
    }

    async void BotPlay()
    {
        currentPlayer = "O";

        var emptyCells = new List<string>();

        if (string.IsNullOrEmpty(B1)) emptyCells.Add("1");
        if (string.IsNullOrEmpty(B2)) emptyCells.Add("2");
        if (string.IsNullOrEmpty(B3)) emptyCells.Add("3");
        if (string.IsNullOrEmpty(B4)) emptyCells.Add("4");
        if (string.IsNullOrEmpty(B5)) emptyCells.Add("5");
        if (string.IsNullOrEmpty(B6)) emptyCells.Add("6");
        if (string.IsNullOrEmpty(B7)) emptyCells.Add("7");
        if (string.IsNullOrEmpty(B8)) emptyCells.Add("8");
        if (string.IsNullOrEmpty(B9)) emptyCells.Add("9");

        if (emptyCells.Count == 0)
        {
            await database.AddResultAsync("Draw");

            Games.Add(new GameResult
            {
                Result = "Draw"
            });

            WeakReferenceMessenger.Default.Send("Draw");

            ResetGame();
            return;
        }

        var cell = emptyCells[random.Next(emptyCells.Count)];

        PlayCell(cell);

        if (CheckWin())
        {
            await database.AddResultAsync("Defeat");

            Games.Add(new GameResult
            {
                Result = "Defeat"           
            });

            WeakReferenceMessenger.Default.Send("Bot wins");

            ResetGame();
            return;
        }

        currentPlayer = "X";
    }

    bool PlayCell(string cell)
    {
        switch (cell)
        {
            case "1": if (string.IsNullOrEmpty(B1)) { B1 = currentPlayer; return true; } break;
            case "2": if (string.IsNullOrEmpty(B2)) { B2 = currentPlayer; return true; } break;
            case "3": if (string.IsNullOrEmpty(B3)) { B3 = currentPlayer; return true; } break;
            case "4": if (string.IsNullOrEmpty(B4)) { B4 = currentPlayer; return true; } break;
            case "5": if (string.IsNullOrEmpty(B5)) { B5 = currentPlayer; return true; } break;
            case "6": if (string.IsNullOrEmpty(B6)) { B6 = currentPlayer; return true; } break;
            case "7": if (string.IsNullOrEmpty(B7)) { B7 = currentPlayer; return true; } break;
            case "8": if (string.IsNullOrEmpty(B8)) { B8 = currentPlayer; return true; } break;
            case "9": if (string.IsNullOrEmpty(B9)) { B9 = currentPlayer; return true; } break;
        }

        return false;
    }

    bool CheckWin()
    {
        if (B1 == B2 && B2 == B3 && !string.IsNullOrEmpty(B1)) return true;
        if (B4 == B5 && B5 == B6 && !string.IsNullOrEmpty(B4)) return true;
        if (B7 == B8 && B8 == B9 && !string.IsNullOrEmpty(B7)) return true;

        if (B1 == B4 && B4 == B7 && !string.IsNullOrEmpty(B1)) return true;
        if (B2 == B5 && B5 == B8 && !string.IsNullOrEmpty(B2)) return true;
        if (B3 == B6 && B6 == B9 && !string.IsNullOrEmpty(B3)) return true;

        if (B1 == B5 && B5 == B9 && !string.IsNullOrEmpty(B1)) return true;
        if (B3 == B5 && B5 == B7 && !string.IsNullOrEmpty(B3)) return true;

        return false;
    }

    void ResetGame()
    {
        B1 = B2 = B3 = B4 = B5 = B6 = B7 = B8 = B9 = "";
        currentPlayer = "X";
    }

    bool IsBoardFull()
    {
        return !string.IsNullOrEmpty(B1) &&
               !string.IsNullOrEmpty(B2) &&
               !string.IsNullOrEmpty(B3) &&
               !string.IsNullOrEmpty(B4) &&
               !string.IsNullOrEmpty(B5) &&
               !string.IsNullOrEmpty(B6) &&
               !string.IsNullOrEmpty(B7) &&
               !string.IsNullOrEmpty(B8) &&
               !string.IsNullOrEmpty(B9);
    }
}