namespace TicTacToe
{
    internal static class TicTacToe
    {
        //The board Game
        static readonly string [,] Board = { {"1", "2", "3"}, {"4", "5", "6"}, {"7", "8", "9"}};
        private static bool _gameFinish;
        private static string _userNow = "You";
        private static  readonly Dictionary<string, string> User = new Dictionary<string, string>()
        {
            { "You", "x" },
            { "Computer", "o" }
        };
        
        private static int _attempt;
        private static void Main()
        {
            Console.WriteLine("Welcome to TicTacToe Game! ^^");
            Console.WriteLine("------------------------------");
            Console.WriteLine("How to play \n1.The unselected Grid will show number \n2.You can type the grid Number \n3.The program is made by beginner \n4.Bug may happen is unconditional situations");
            
            //The main game is here ^^
            while (!_gameFinish)
            {
                if (_attempt >= 9)
                {
                    Console.WriteLine("It's a Tie! How can you lose with a dumb bot :v");
                    _gameFinish = true;
                    break; // Exit the loop
                }

                Thread.Sleep(1000); // Cooldown
                DisplayBoard(Board); // Display board
                Console.WriteLine("------------------------------");
                Console.WriteLine("Please write the number of grid: ");
                _userNow = "You";

                try
                {
                    int userInput = Convert.ToInt16(Console.ReadLine());
                    if (!UserChoice(Board, userInput)) continue; // Skip invalid move
                    _attempt++;

                    // Check win condition for Player
                    if (User.TryGetValue(_userNow, out var name) && WinChecker(Board, name))
                    {
                        DisplayBoard(Board);
                        Console.WriteLine("------------------------------");
                        Console.WriteLine($"Congratulations! {name} Won the game!");
                        _gameFinish = true;
                        break; // Exit the loop
                    }

                    // Computer's turn
                    _userNow = "Computer";
                    ComputerTurn(Board);
                    _attempt++;

                    // Check win condition for Computer
                    if (User.TryGetValue(_userNow, out name) && WinChecker(Board, name))
                    {
                        DisplayBoard(Board);
                        Console.WriteLine("------------------------------");
                        Console.WriteLine($"The Computer ({name}) Won the game!");
                        _gameFinish = true;
                        break; // Exit the loop
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("------------------------------");
                    Console.WriteLine($"Please enter a valid number! | {e.Message}");
                }
            }
            Console.WriteLine("Thanks for playing my game ^^");
            Console.ReadKey();

        }
        
        /**
         * Will display the board in terminal
         */
        static void DisplayBoard(string[,] data)
        {
            Console.WriteLine("------------------------------");
            for (var i = 0; i < 3; i++)
            {
                Console.WriteLine($"{data[i, 0]} | {data[i, 1]} | {data[i, 2]}");
                if (i < 2) Console.WriteLine("--+---+--");
            }
        }

        /**
         * This function process what user input
         */
        static bool UserChoice(string[,] data, int userInput)
        {
            var isUpdated = false;
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (userInput.ToString() == data[i, j])
                    {
                        data[i, j] = "x";
                        isUpdated = true;
                        break;
                    }
                }
                if(isUpdated) break;
            }
            if (!isUpdated)
            {
                Console.WriteLine("------------------------------");
                Console.WriteLine("Invalid move! The position is already taken or out of bounds.");
                return false;
            }
            return true;
        }

        /**
         * It always checks win condition in a realtime :v
         */
        static bool WinChecker(string[,] board, string player)
        {
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == player && board[i, 1] == player && board[i, 2] == player) return true; // Horizontal
                if (board[0, i] == player && board[1, i] == player && board[2, i] == player) return true; // Vertical
            }
            if (board[0, 0] == player && board[1, 1] == player && board[2, 2] == player) return true; // Diagonal
            if (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player) return true; // Diagonal
            return false;

        }

        /**
         * Allow to select random grid and paste it with o (he quite dumb)
         */
        static void ComputerTurn(string[,] board)
        {
            var random = new Random();
            while (true)
            {
                // Randomly select a position between 1 and 9
                var index = random.Next(1, 10);

                // Find corresponding row and column
                int row = (index - 1) / 3;
                int col = (index - 1) % 3;

                // Check if the position is empty
                if (board[row, col] != "x" && board[row, col] != "o")
                {
                    board[row, col] = "o"; // Place the computer's move
                    break; // Exit the loop after making a move
                }
            }
        }
    }
}

