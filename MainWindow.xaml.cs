using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string currentPlayer = "X";
        private int xScore = 0, oScore = 0, catsScore = 0;
        private Button[,] buttons;

        public MainWindow()
        {
            InitializeComponent();
            buttons = new Button[3, 3] { { btn00, btn01, btn02 }, { btn10, btn11, btn12 }, { btn20, btn21, btn22 } };
            lblCurrentPlayer.Text = currentPlayer;
        }


        private void ChooseStartingPlayer_Click(object sender, RoutedEventArgs e)
        {
            currentPlayer = new Random().Next(0, 2) == 0 ? "X" : "O";
            lblCurrentPlayer.Text = currentPlayer;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            clickedButton.Content = currentPlayer;
            clickedButton.IsEnabled = false;

            // Check for a winner or if the board is full
            if (CheckWinner())
            {
                string winnerName = currentPlayer == "X" ? txtXPlayer.Text : txtOPlayer.Text;
                MessageBox.Show($"{winnerName} ({currentPlayer}) is the Winner!", "Winner Announcement");
                UpdateScore(); // Update the winners score
                ResetBoard(); // Reset the game board after a win
            }
            else if (IsBoardFull())
            {
                MessageBox.Show("It's a tie!", "Game Over");
                catsScore++;
                lblCatsScore.Text = catsScore.ToString();
                ResetBoard(); // Reset the game board after a tie
            }

            // Switch player
            currentPlayer = currentPlayer == "X" ? "O" : "X";
            lblCurrentPlayer.Text = currentPlayer == "X" ? txtXPlayer.Text : txtOPlayer.Text;
        }

        private void UpdateScore()
        {
            if (currentPlayer == "X")
            {
                xScore++;
                lblXScore.Text = xScore.ToString();
            }
            else
            {
                oScore++;
                lblOScore.Text = oScore.ToString();
            }
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            ResetBoard();
        }

        private void ResetBoard()
        {
            throw new NotImplementedException();
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // Check if the board is full (for a tie)
        private bool IsBoardFull()
        {
            foreach (var button in buttons)
            {
                if (button.IsEnabled) return false; // If any button is enabled, the board is not full
            }
            return true;
        }
        // Check for a winner
        private bool CheckWinner()
        {
            // Check rows, columns, and diagonals for a winning combination
            for (int i = 0; i < 3; i++)
            {
                // Check rows
                if (buttons[i, 0].Content != null &&
                    buttons[i, 0].Content == buttons[i, 1].Content &&
                    buttons[i, 1].Content == buttons[i, 2].Content)
                {
                    return true;
                }

                // Check columns
                if (buttons[i, 0].Content != null &&
                    buttons[i, 0].Content == buttons[i, 1].Content &&
                    buttons[i, 1].Content == buttons[2, i].Content)
                {
                    return true;
                }
            }

            // Check diagonals
            if (buttons[0, 0].Content != null &&
                buttons[0, 0].Content == buttons[1, 1].Content &&
                buttons[1, 1].Content == buttons[2, 2].Content)
            {
                return true;
            }

            if (buttons[0, 2].Content != null &&
                buttons[0, 2].Content == buttons[1, 2].Content &&
                buttons[1, 1].Content == buttons[2, 0].Content)
            {
                return true;
            }
            return false; // No winner found
        }
    }
}