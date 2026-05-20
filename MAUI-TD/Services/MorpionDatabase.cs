using SQLite;
using MAUI_TD.Models;

namespace MAUI_TD.Services;

public class MorpionDatabase
{
    SQLiteAsyncConnection database;

    async Task Init()
    {
        if (database != null)
            return;

        database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);

        await database.CreateTableAsync<GameResult>();
    }

    public async Task<List<GameResult>> GetHistoryAsync()
    {
        await Init();
        return await database.Table<GameResult>().ToListAsync();
    }

    public async Task AddResultAsync(string result)
    {
        await Init();

        var game = new GameResult
        {
            Result = result        };

        await database.InsertAsync(game);
    }
}