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

    public ObservableCollection<GameResult> Games { get; } = new();

    private readonly MorpionDatabase _database;
    private readonly MorpionApiService _api;

    private Guid? _partieId = null;
    private bool _enAttente = false;

    public MainViewModel(MorpionDatabase database, MorpionApiService api)
    {
        _database = database;
        _api = api;
        LoadHistory();
    }

    async void LoadHistory()
    {
        var games = await _database.GetHistoryAsync();
        foreach (var g in games)
            Games.Add(g);
    }

    [RelayCommand]
    async Task Play(string cell)
    {
        if (_enAttente) return;

        
        if (!int.TryParse(cell, out int num)) return;
        int caseIndex = num - 1;

        if (_partieId == null)
        {
            _enAttente = true;
            var nouvelle = await _api.CreerPartieAsync();
            _enAttente = false;

            if (nouvelle == null)
            {
                WeakReferenceMessenger.Default.Send("Error : Can't access the API");
                return;
            }

            _partieId = nouvelle.Id;
        }

        
        _enAttente = true;
        var partie = await _api.JouerCoupAsync(_partieId.Value, caseIndex);
        _enAttente = false;

        if (partie == null) return; 

        
        MettreAJourPlateau(partie.Plateau);

        
        if (partie.Terminee)
        {
            string resultat;

            if (partie.Gagnant == "X")
                resultat = "Player X wins";
            else if (partie.Gagnant == "O")
                resultat = "Defeat";
            else
                resultat = "Draw";

            await _database.AddResultAsync(resultat);
            Games.Add(new GameResult { Result = resultat });
            WeakReferenceMessenger.Default.Send(resultat);

            
            _partieId = null;
            ResetPlateau();
        }
    }

    void MettreAJourPlateau(string?[] plateau)
    {
        B1 = plateau[0] ?? "";
        B2 = plateau[1] ?? "";
        B3 = plateau[2] ?? "";
        B4 = plateau[3] ?? "";
        B5 = plateau[4] ?? "";
        B6 = plateau[5] ?? "";
        B7 = plateau[6] ?? "";
        B8 = plateau[7] ?? "";
        B9 = plateau[8] ?? "";
    }

    void ResetPlateau()
    {
        B1 = B2 = B3 = B4 = B5 = B6 = B7 = B8 = B9 = "";
    }
}