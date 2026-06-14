using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace MAUI_TD.Services;

public class MorpionApiService
{
    private readonly HttpClient _http;
    private const string BaseUrl = "http://localhost:5045/api";

    public MorpionApiService()
    {
        _http = new HttpClient();
    }

    // Stock JWT
    public void SetToken(string token)
    {
        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<string?> LoginAsync(string email, string password)
    {
        var response = await _http.PostAsJsonAsync($"{BaseUrl}/auth/login", new
        {
            email,
            password
        });

        if (!response.IsSuccessStatusCode) return null;

        var result = await response.Content.ReadFromJsonAsync<TokenDto>();
        return result?.Token;
    }

    public async Task<string?> RegisterAsync(string email, string password)
    {
        var response = await _http.PostAsJsonAsync($"{BaseUrl}/auth/register", new
        {
            email,
            password
        });

        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<PartieDto?> CreerPartieAsync()
    {
        var response = await _http.PostAsync($"{BaseUrl}/morpion/creer", null);
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadFromJsonAsync<PartieDto>();
    }

    public async Task<PartieDto?> JouerCoupAsync(Guid partieId, int caseIndex)
    {
        var response = await _http.PostAsync($"{BaseUrl}/morpion/{partieId}/jouer/{caseIndex}", null);
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadFromJsonAsync<PartieDto>();
    }

    public async Task<PartieDto?> ObtenirPartieAsync(Guid partieId)
    {
        return await _http.GetFromJsonAsync<PartieDto>($"{BaseUrl}/morpion/{partieId}");
    }
}

public class TokenDto
{
    public string Token { get; set; } = "";
}

public class PartieDto
{
    public Guid Id { get; set; }
    public string?[] Plateau { get; set; } = new string?[9];
    public string Tour { get; set; } = "X";
    public string? Gagnant { get; set; }
    public bool Terminee { get; set; }
}