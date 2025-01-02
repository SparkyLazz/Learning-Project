# Odd or Even Checker

## Description
The **Odd or Even Checker** is a terminal-based program that determines whether a user-provided number is odd or even. It validates user input, ensures only whole numbers are processed, and allows users to repeatedly check numbers until they choose to exit.

## Features
1. **Odd or Even Determination**:
   - Determines if a number is odd or even using the modulus operator (`%`).
2. **Input Validation**:
   - Ensures the input is numeric.
   - Accepts only whole numbers for processing.
3. **Interactive User Interface**:
   - Prompts the user to input a number.
   - Allows the user to perform multiple checks in one session.
4. **Exit Option**:
   - Lets the user gracefully exit the program.

## How the Script Works
### Input a Number
1. The program prompts the user to input a number.
2. A `try-except` block ensures that only numeric inputs are accepted:
   ```python
   try:
       userInput = float(input("Please put a random number: "))
   except ValueError:
       print("Invalid Number!")
       continue
   ```

### Validate the Number
1. The program checks if the input is a whole number:
   ```python
   if not userInput.is_integer():
       print("Please enter a whole number to check for Odd or Even.")
       continue
   ```
2. If the input passes validation, it is converted to an integer:
   ```python
   userInput = int(userInput)
   ```

### Determine Odd or Even
1. The program uses the modulus operator (`%`) to determine divisibility by 2:
   - Odd: `userInput % 2 != 0`
   - Even: `userInput % 2 == 0`
2. Displays the result to the user:
   ```python
   if userInput % 2 != 0:
       print(f"Number of {userInput} is Odd")
   else:
       print(f"Number of {userInput} is Even")
   ```

### Repeated Checks or Exit
1. After each check, the program asks the user if they want to check another number:
   ```python
   more = input("Do you want to perform another check? (yes/no): ").lower()
   ```
2. The loop continues if the user types `yes`. Otherwise, the program thanks the user and exits.

## How to Run
1. Save the script as `odd_or_even_checker.py`.
2. Open a terminal and navigate to the script's directory.
3. Run the script with:
   ```bash
   python odd_or_even_checker.py
   ```
4. Follow the prompts to input numbers and determine if they are odd or even.

## Example Run
```
Please put a random number: 7
------------------------------------------------
Number of 7 is Odd
------------------------------------------------
Do you want to perform another check? (yes/no): yes

Please put a random number: 4
------------------------------------------------
Number of 4 is Even
------------------------------------------------
Do you want to perform another check? (yes/no): no
------------------------------------------------
Thank you for using the Checker!
