using SQLite;

namespace MAUI_TD.Models;

public class GameHistory
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Result { get; set; }

}