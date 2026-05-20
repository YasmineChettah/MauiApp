using System.Net.Http.Json;

namespace MAUI_TD.Services;

public class MorpionApiService
{
    private readonly HttpClient _http;

    private const string BaseUrl = "http://localhost:5045/api/morpion";

    public MorpionApiService()
    {
        _http = new HttpClient();
    }

    public async Task<PartieDto?> CreerPartieAsync()
    {
        var response = await _http.PostAsync($"{BaseUrl}/creer", null);
        return await response.Content.ReadFromJsonAsync<PartieDto>();
    }

    public async Task<PartieDto?> JouerCoupAsync(Guid partieId, int caseIndex)
    {
        var response = await _http.PostAsync($"{BaseUrl}/{partieId}/jouer/{caseIndex}", null);
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadFromJsonAsync<PartieDto>();
    }

    public async Task<PartieDto?> ObtenirPartieAsync(Guid partieId)
    {
        return await _http.GetFromJsonAsync<PartieDto>($"{BaseUrl}/{partieId}");
    }
}


public class PartieDto
{
    public Guid Id { get; set; }
    public string?[] Plateau { get; set; } = new string?[9];
    public string Tour { get; set; } = "X";
    public string? Gagnant { get; set; }
    public bool Terminee { get; set; }
}