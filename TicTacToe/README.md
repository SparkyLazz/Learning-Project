# Tic Tac Toe Game (C#)

## Description
The **Tic Tac Toe Game** is a console-based implementation of the classic two-player game. In this version, the player competes against the computer to mark three cells in a row, column, or diagonal. The game also handles tie conditions when the board is full.

## Features
1. **Game Board**:
   - A 3x3 grid displayed in the terminal.
   - Players can select positions by entering numbers (1–9).

2. **Player Interaction**:
   - The player uses `X` while the computer uses `O`.
   - Input validation ensures correct and valid moves.

3. **Win/Tie Conditions**:
   - Automatically detects wins (rows, columns, diagonals) or ties when the board is full.

4. **Random Computer Moves**:
   - The computer selects its moves randomly, avoiding already occupied cells.

## How the Script Works
### Initialization
- The game initializes a 3x3 grid represented as:
  ```
  1 | 2 | 3
  --+---+--
  4 | 5 | 6
  --+---+--
  7 | 8 | 9
  ```
- Empty cells are numbered (1–9) to guide user input.

### Game Loop
- Alternates between the player and the computer.
- Checks for win or tie conditions after every turn.
- Displays the updated board after each move.

### Tie Checker
- If the board is full (9 moves) and no winner is detected, the game declares a tie.

## How to Run
1. Save the file as `TicTacToe.cs`.
2. Compile and run the program:
   ```bash
   csc TicTacToe.cs && TicTacToe.exe
   ```
3. Follow the on-screen instructions to play.

## Functions
### `DisplayBoard`
Displays the current state of the board in the terminal.
```csharp
static void DisplayBoard(string[,] data)
```
- Loops through the `board` array to print the 3x3 grid.
- Includes separators (`--+---+--`) between rows.

### `UserChoice`
Processes the player’s input and updates the board if the move is valid.
```csharp
static bool UserChoice(string[,] data, int userInput)
```
- Checks if the selected cell matches the input number.
- Updates the board with `X` if valid, otherwise prompts for a retry.

### `ComputerTurn`
Randomly selects an empty cell and updates the board with the computer’s move.
```csharp
static void ComputerTurn(string[,] board)
```
- Uses `Random.Next(1, 10)` to pick a number.
- Maps the number to the corresponding row and column.
- Ensures no overwriting of existing marks (`X` or `O`).

### `WinChecker`
Evaluates the board for winning conditions (rows, columns, diagonals).
```csharp
static bool WinChecker(string[,] board, string player)
```
- Loops through rows and columns to check for three matching marks.
- Checks diagonals for the same condition.
- Returns `true` if a win is detected.

### `Main`
Controls the game loop, including input handling, win/tie checks, and alternating turns.
```csharp
private static void Main()
```
- Handles user input and ensures the game progresses smoothly.
- Increments the `_attempt` counter after every valid move.
- Declares the winner or tie and exits the loop accordingly.

## Example Output
```
Welcome to TicTacToe Game! ^^
------------------------------
How to play 
1. The unselected Grid will show number 
2. You can type the grid Number 
3. The program is made by beginner 
4. Bug may happen is unconditional situations

1 | 2 | 3
--+---+--
4 | 5 | 6
--+---+--
7 | 8 | 9

Please write the number of grid: 5
You chose: 5
Computer chose: 3

1 | 2 | O
--+---+--
4 | X | 6
--+---+--
7 | 8 | 9
```
