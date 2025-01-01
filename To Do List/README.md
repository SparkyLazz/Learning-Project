# To-Do List Application

## Description
The **To-Do List Application** is a terminal-based program that helps users manage tasks efficiently. Users can add, view, filter, complete, and delete tasks while ensuring data persistence in a JSON file.

---

## Features
1. **Add Task**:
   - Users can create tasks with a name, description, deadline, and priority.
2. **View Tasks**:
   - Display tasks with options to filter by date, priority, or no filter.
3. **Mark as Complete**:
   - Update a task's status to "Completed."
4. **Delete Task**:
   - Remove a task permanently from the list.
5. **Data Persistence**:
   - Tasks are saved in a JSON file and loaded automatically at startup.

---

## How It Works

### **1. Main Menu**
The main menu provides the following options:
- Add Task
- View Task
- Mark Task as Complete
- Delete Task
- Exit

Users can navigate through the menu by entering the corresponding option number.

---

### **2. Functions**

#### **AddTask()**
- **Purpose**: Prompts the user to add a new task.
- **Steps**:
  1. Collect task details such as name, description, deadline, and priority.
  2. Validate user inputs, assigning defaults if necessary.
  3. Add the task to the task list and save it to the JSON file.
- **Example**:
  ```
  Task Name (default: Task): Complete Project
  Description (default: None): Finish the client report
  Deadline (default: Now | Format: YYYY-MM-DD): 2024-12-31
  Priority (High / Medium / Low | default: Medium): High
  Are you sure (yes / no | else = no): yes
  [-] Successfully added new task to storage.
  ```

#### **ViewTask()**
- **Purpose**: Displays all tasks with filtering options.
- **Steps**:
  1. Ask the user to choose a filter (By Date, By Priority, No Filter).
  2. Display tasks based on the selected filter in a formatted table.
  3. Allow the user to select a task for more details.
- **Example**:
  ```
  Choose the filter:
  1. By A-Z
  2. By Date
  3. By Priority
  4. No Filter | Hit Enter

  ID   Name                  Deadline               Priority   Status
  1    Complete Project      2024-12-31            High       On Going
  ```

#### **MarkAsCompleteTask()**
- **Purpose**: Allows users to mark a specific task as completed.
- **Steps**:
  1. Display all tasks.
  2. Prompt the user to select a task by ID.
  3. Update the task's status to "Completed" and save changes.

#### **DeleteTask()**
- **Purpose**: Deletes a task from the list.
- **Steps**:
  1. Display all tasks.
  2. Prompt the user to select a task by ID.
  3. Remove the task and save the updated list.

#### **Filter Functions**
- **NoFilter()**: Displays all tasks without filters.
- **IdFilter()**: Sorts tasks alphabetically by name.
- **DateFilter()**: Sorts tasks by upcoming deadlines.
- **PriorityFilter()**: Sorts tasks by priority (High > Medium > Low).

#### **Utility Functions**
- **GetJsonFile()**:
  - Locates or creates the `storage.json` file for task persistence.
- **SaveFile(string jsonPath)**:
  - Serializes the task list and saves it to a JSON file.
- **LoadFile(string jsonPath)**:
  - Loads task data from the JSON file into the program at startup.
- **FocusedSelection()**:
  - Displays detailed information about a specific task selected by the user.

---

## Example Task Data (JSON)
```json
[
  { "Id": 1, "Name": "Complete Project", "Description": "Finish the client report", "Deadline": "2024-12-31", "Priority": "High", "IsComplete": false },
  { "Id": 2, "Name": "Grocery Shopping", "Description": "Buy groceries", "Deadline": "2024-12-25", "Priority": "Medium", "IsComplete": false }
]
```

---

## How to Run
1. Save the script as `ToDoList.cs`.
2. Place the `storage.json` file in the project directory.
3. Compile and run the program:
   ```bash
   dotnet run
   ```

---

## Example Usage
1. **Add Task**:
   ```
   Task Name (default: Task): Finish Homework
   Description (default: None): Math exercises
   Deadline (default: Now | Format: YYYY-MM-DD): 2024-12-15
   Priority (High / Medium / Low | default: Medium): Low
   Are you sure (yes / no | else = no): yes
   [-] Successfully added new task to storage.
   ```

2. **View Task**:
   ```
   Choose the filter:
   1. By A-Z
   2. By Date
   3. By Priority
   4. No Filter | Hit Enter

   ID   Name                  Deadline               Priority   Status
   1    Complete Project      2024-12-31            High       On Going
   2    Grocery Shopping      2024-12-25            Medium     On Going
   ```

3. **Mark Task as Complete**:
   ```
   Enter Task ID to mark as complete: 1
   Task "Complete Project" marked as completed.
   ```
