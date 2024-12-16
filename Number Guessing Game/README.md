# Number Guessing Game

## Description
The **Number Guessing Game** is a terminal-based program where the user guesses a randomly selected number between 1 and 10. The program provides feedback on whether the guess is too high, too low, or correct. The player has a limited number of attempts to guess the correct number.

## Features
1. Generates a random number between 1 and 10.
2. Allows the user to input guesses.
3. Provides feedback on whether the guess is:
   - Too high.
   - Too low.
   - Correct.
4. Limits the player to 5 attempts.
5. Validates user input to ensure it is within the expected range.
6. Handles invalid input gracefully (e.g., non-numeric values).
7. Displays a winning or losing message at the end of the game.

## How the Script Works
### Random Number Generation
```python
randomNumber = random.randrange(1, 11)
```
- This generates a random integer between 1 and 10 (inclusive) and assigns it to the variable `randomNumber`.

### Attempts Counter
```python
change = 5
```
- The variable `change` tracks how many attempts the user has left. It starts at 5 and decreases with each valid guess.

### Game Loop
```python
while not win:
```
- The loop continues until the user either:
  1. Guesses the correct number (`win = True`).
  2. Runs out of attempts (`change == 0`).

### Input Validation
```python
if not (1 <= userInput <= 10):
```
- Ensures the user’s input is a number between 1 and 10. If it’s outside this range, the program prompts the user again without deducting an attempt.

### Feedback and Conditions
- If the guess is too high:
  ```python
  if userInput > randomNumber:
      print(f"> The number is too high | Change: {change}")
  ```
- If the guess is too low:
  ```python
  if userInput < randomNumber:
      print(f"> The number is too low | Change: {change}")
  ```
- If the guess is correct:
  ```python
  if userInput == randomNumber:
      print(f"> Yeah, the number is: {randomNumber}")
      print("> You Won!")
      win = True
  ```

### Graceful Handling of Invalid Input
```python
except ValueError:
    print("Please enter a valid number.")
```
- Ensures the program does not crash if the user enters something other than a number.

### End of Game
- If the player runs out of attempts:
  ```python
  if change == 0:
      print("You don’t have any chances left! | You lose")
      break
  ```
- Displays a thank-you message at the end of the game:
  ```python
  print("Thank you for playing! ^^")
  ```

## How to Run
1. Make sure Python is installed on your system.
2. Save the script as `main.py`.
3. Open a terminal and navigate to the script's directory.
4. Run the script with:
   ```bash
   python main.py
   ```
5. Follow the on-screen instructions to play the game.

## Future Improvements
- Add difficulty levels (e.g., easy mode with unlimited attempts, hard mode with fewer attempts).
- Track the user’s high score (minimum attempts to win).
- Expand the range of numbers for advanced difficulty levels.
- Include a replay option at the end of the game.

## Contact
If you have feedback or suggestions for this project, feel free to reach out!
