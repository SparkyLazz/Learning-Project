## Rock, Paper, Scissors Game

## Description
The **Rock, Paper, Scissors Game** is a terminal-based game where the user competes against the computer. The computer randomly selects an action (rock, paper, or scissors), and the program determines the winner based on predefined game rules.

## Features
1. **Game Logic**:
   - Rock beats Scissors.
   - Scissors beats Paper.
   - Paper beats Rock.
   - Ties occur if both choices are the same.
2. **Input Validation**:
   - Ensures user input matches one of the valid options.
3. **Randomized Computer Choice**:
   - The computer selects its choice randomly.
4. **Replay Option**:
   - The user can play multiple rounds.

## Functions Explained
### Randomized Computer Choice
```python
computerChoice = random.choice(list(rules.keys()))
```
- This line uses `random.choice()` to randomly select one of the valid actions (rock, paper, scissors).

### Game Rules Using a Dictionary
```python
rules = {
    "rock": {"win": "scissors", "lose": "paper"},
    "paper": {"win": "rock", "lose": "scissors"},
    "scissors": {"win": "paper", "lose": "rock"}
}
```
- A dictionary defines the win/lose relationships for each action:
   - `"win"` shows what the user beats.
   - `"lose"` shows what the user loses to.

### Input Validation
```python
if userInput not in rules:
    print("Invalid action! Please choose one of: rock, paper, scissors.")
    continue
```
- Checks if the user's input is valid and provides feedback if it’s not.

### Determining the Winner
```python
if computerChoice == rules[userInput]["lose"]:
    print("You lose the game!")
elif userInput == computerChoice:
    print("Game tie!")
elif computerChoice == rules[userInput]["win"]:
    print("You win the game!")
```
- Compares the user's choice to the computer's choice:
   - Checks if the user wins, loses, or ties based on the rules.

### Replay Option
```python
more = input("Do you want to perform another play? (yes/no): ").lower()
if more != "yes":
    break
```
- Prompts the user to play another round or exit the game.

## How to Run
1. Save the script as `rock_paper_scissors.py`.
2. Open a terminal and run the script:
   ```bash
   python rock_paper_scissors.py
   ```
3. Follow the prompts to play the game.

## Example Run
```
Choose your action [rock, paper, scissors]: rock
-------------------------------------------------------------
You win the game!
> Computer choice: scissors | Your choice: rock
-------------------------------------------------------------
Do you want to perform another play? (yes/no): no
------------------------------------------------
Thank you for using the game!
