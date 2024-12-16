## Calculator Application

### Description
This program is a simple terminal-based calculator that performs basic arithmetic operations, such as addition, subtraction, multiplication, and division. It allows users to perform calculations repeatedly until they choose to exit.

### Features
1. **Arithmetic Operations**:
   - Addition (`+`)
   - Subtraction (`-`)
   - Multiplication (`*`)
   - Division (`/`)
2. **Graceful Error Handling**:
   - Ensures inputs are numeric.
   - Prevents division by zero.
3. **Interactive User Interface**:
   - Displays prompts and clear instructions.
   - Allows repeated calculations until the user decides to exit.

### How the Script Works
1. **Input Numbers**:
   - Prompts the user to enter two numbers.
   - Validates input to ensure it is numeric using a `try-except` block.

   ```python
   try:
       firstNumber = float(input("Please enter the first Number : "))
       secondNumber = float(input("Please enter the second Number : "))
   except ValueError:
       print("Invalid Number | Please provide a valid Number")
       continue
   ```

2. **Select an Operation**:
   - The user chooses an operation (`+`, `-`, `*`, `/`).
   - The program validates the choice and performs the corresponding calculation.

   ```python
   action = input("| + | - | / | * | Please choose your action for both numbers: ")

   if action == "+":
       result = firstNumber + secondNumber
   elif action == "-":
       result = firstNumber - secondNumber
   elif action == "/":
       if secondNumber == 0:
           print("Division by zero is not allowed!")
           continue
       result = firstNumber / secondNumber
   elif action == "*":
       result = firstNumber * secondNumber
   else:
       print("Invalid Action!")
       continue
   ```

3. **Display Result**:
   - The result of the operation is displayed in a clear format:

   ```python
   print(f"The result is : {result}")
   ```

4. **Repeat or Exit**:
   - After displaying the result, the program asks the user if they want to perform another calculation. The loop continues unless the user enters `no`.

   ```python
   restart = input("Do you want to perform another calculation? (yes/no): ").lower()
   if restart != "yes":
       print("Thank you for using the calculator!")
       break
   ```

### How to Run
1. Save the script as `calculator.py`.
2. Open a terminal and navigate to the script's directory.
3. Run the script with:
   ```bash
   python calculator.py
   ```
4. Follow the on-screen instructions to perform calculations.

### Example Run
```
Please enter the first Number : 10
Please enter the second Number : 2
| + | - | / | * | Please choose your action for both numbers: /
The result is : 5.0
Do you want to perform another calculation? (yes/no): no
Thank you for using the calculator!
```
