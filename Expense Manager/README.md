# Expense Manager

## Description
The **Expense Manager** is a console-based application designed to track expenses effectively. It allows users to add, view, and delete expenses while storing the data in a JSON file (`SaveData.json`). This ensures persistence between program runs.

## Features
1. **Add Expense**:
    - Record a new expense with a description, amount, and date.
    - Auto-assigns a unique ID to each expense.

2. **View Expenses**:
    - Displays all recorded expenses in a tabular format.

3. **Delete Expense**:
    - Removes an expense by its ID.

4. **Save and Load Data**:
    - Persists expenses in a JSON file (`SaveData.json`).
    - Automatically loads existing data on startup.

5. **Dynamic Path Resolution**:
    - Searches directories dynamically to locate the `SaveData.json` file.

## Functions Explained

### **`Main`**
The main entry point of the program.
- Displays a menu with options: Add Expense, View Expense, Delete Expense, and Exit.
- Manages user input and calls the appropriate function based on the selected option.

### **`AddExpense`**
Handles the addition of a new expense.
- Prompts the user for:
    - `Description` (default: "item")
    - `Amount` (default: `1`)
    - `Date` (default: current date).
- Auto-generates a unique ID based on the last expense in the list.
- Saves the updated list to `SaveData.json`.

### **`ViewExpense`**
Displays all expenses in a neatly formatted table.
- Columns include `ID`, `Description`, `Amount`, and `Date`.
- Uses formatted output for alignment.

### **`DeleteExpense`**
Removes an expense by its ID.
- Prompts the user to enter the ID.
- If a matching expense is found, it is removed from the list.
- Saves the updated list to `SaveData.json`.

### **`SetUp`**
Prepares the application environment on startup.
- Searches for the `SaveData.json` file in the parent directory.
- If found, loads the existing data.
- Provides feedback about the loading process.

### **`SaveFile`**
Saves the current list of expenses to `SaveData.json`.
- Converts the expense list to JSON format using `JsonConvert.SerializeObject`.
- Writes the JSON string to the file.

### **`LoadExpenses`**
Loads existing expenses from `SaveData.json`.
- Reads and parses the JSON file to populate the `_expenses` list.
- Ensures no data is lost by initializing an empty list if the file is empty or missing.

### **Expense Class**
Defines the structure of an expense.
- Fields:
    - `Id` (unique identifier for each expense).
    - `Description` (description of the expense).
    - `Amount` (expense amount).
    - `Date` (date of the expense).

## How to Run
1. Save the files and ensure `SaveData.json` exists in the expected directory.
2. Compile and run the application:
   ```bash
   dotnet run
   ```
3. Follow the on-screen prompts to manage expenses.

## Example Output
```
Welcome to Expense Manager
1. Add Expense
2. View Expense
3. Delete Expense
4. Exit
--------------------------
Choose an option : 2

ID    Description          Amount     Date
--------------------------------------------------
1     Groceries            Rp46.00    2024-12-21
2     Electricity Bill     Rp75.00    2024-12-20
