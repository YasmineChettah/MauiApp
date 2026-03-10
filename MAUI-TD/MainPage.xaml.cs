namespace MAUI_TD;

public partial class MainPage : ContentPage
{
    string currentPlayer = "X";

    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnCellClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;

        if (!string.IsNullOrEmpty(button.Text))
            return;

        button.Text = currentPlayer;

        if (CheckWinner())
        {
            await DisplayAlertAsync("Winner", $"Player {currentPlayer} wins!", "OK");
            ResetGame();
            return;
        }

        currentPlayer = currentPlayer == "X" ? "O" : "X";
    }

    bool CheckWinner()
    {
        // lines
        if (B1.Text == B2.Text && B2.Text == B3.Text && !string.IsNullOrEmpty(B1.Text)) return true;
        if (B4.Text == B5.Text && B5.Text == B6.Text && !string.IsNullOrEmpty(B4.Text)) return true;
        if (B7.Text == B8.Text && B8.Text == B9.Text && !string.IsNullOrEmpty(B7.Text)) return true;

        // columns
        if (B1.Text == B4.Text && B4.Text == B7.Text && !string.IsNullOrEmpty(B1.Text)) return true;
        if (B2.Text == B5.Text && B5.Text == B8.Text && !string.IsNullOrEmpty(B2.Text)) return true;
        if (B3.Text == B6.Text && B6.Text == B9.Text && !string.IsNullOrEmpty(B3.Text)) return true;

        // diago
        if (B1.Text == B5.Text && B5.Text == B9.Text && !string.IsNullOrEmpty(B1.Text)) return true;
        if (B3.Text == B5.Text && B5.Text == B7.Text && !string.IsNullOrEmpty(B3.Text)) return true;

        return false;
    }

    void ResetGame()
    {
        B1.Text = "";
        B2.Text = "";
        B3.Text = "";
        B4.Text = "";
        B5.Text = "";
        B6.Text = "";
        B7.Text = "";
        B8.Text = "";
        B9.Text = "";

        currentPlayer = "X";
    }
}