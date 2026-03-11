using MAUI_TD.Models;

namespace MAUI_TD.Services;

public class FakeGameHistory
{
    private List<GameResult> history = new();

    public void AddResult(string result)
    {
        history.Add(new GameResult{Result = result});
    }

    public List<GameResult> GetHistory()
    {
        return history;
    }
}