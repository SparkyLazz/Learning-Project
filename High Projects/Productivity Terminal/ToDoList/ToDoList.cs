using System.Globalization;
namespace Productivity_Terminal.ToDoList
{
    public class ToDoList
    {
        public required long Id;
        public required string Name;
        public required string Description;
        public required DateTime DeadLine;

        public required string Priority;
        public required bool IsComplete;
        
        public static void ToDoListMenu(List<ToDoList> toDoLists)
        {
            var isExit = false;
            while (!isExit)
            {
                
                Console.WriteLine("1. Add Task");
                Console.WriteLine("2. View Task");
                Console.WriteLine("3. Mark as Complete");
                Console.WriteLine("4. Delete Task");
                Console.WriteLine("5. Back");
                Console.WriteLine("========================================================================================");
                Console.Write("Your action : ");

                string userInput = Console.ReadLine() ?? String.Empty;
                switch (userInput)
                {
                    case "1":
                        Console.Clear();
                        AddTask(toDoLists);
                        break;
                    case "2":
                        Console.Clear();
                        ViewTask(toDoLists);
                        break;
                    case "3":
                        Console.Clear();
                        MarkAsComplete(toDoLists);
                        break;
                    case "4":
                        Console.Clear();
                        DeleteTask(toDoLists);
                        break;
                    case "5":
                        isExit = true;
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("[!] Invalid Input | Enter to continue");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }

        private static void AddTask(List<ToDoList> toDoLists)
        {
            bool isDone = false;
            while (!isDone)
            {
                //Task name 
                Console.Write("Task Name (default : Task) : ");
                string nameTask = Console.ReadLine() ?? String.Empty;
                nameTask = string.IsNullOrEmpty(nameTask) ? "Task" : nameTask;
                
                //Description name
                Console.Write("Description (default : No Description) : ");
                string descriptionTask = Console.ReadLine() ?? String.Empty;
                descriptionTask = string.IsNullOrEmpty(descriptionTask) ? "No Description" : descriptionTask;
                
                //Deadline 
                Console.Write("Deadline (default : Now | Format : YYYY-MM-DD ) : ");
                string deadlineString = Console.ReadLine() ?? String.Empty;
                
                DateTime deadlineTask = string.IsNullOrEmpty(deadlineString)
                    ? DateTime.Now
                    : DateTime.ParseExact(deadlineString, "yyyy-MM-dd", null);
                
                //Priority
                Console.Write("Priority (High / Medium / Low | default : Medium ) : ");
                string priorityTask = Console.ReadLine() ?? String.Empty;
                priorityTask = string.IsNullOrEmpty(priorityTask) ? "Medium" : priorityTask;
                
                //Clean
                Console.Clear();
                
                //Verify User Input
                Console.WriteLine("======================================Result========================================");
                Console.WriteLine("Name Task : " + nameTask);
                Console.WriteLine("Description Task : " + descriptionTask);
                Console.WriteLine("Deadline : " + deadlineTask.ToString("yyyy-MM-dd"));
                Console.WriteLine("Priority : " + priorityTask);
                Console.WriteLine("====================================================================================");
                Console.Write("Verify your input (yes / no ) : ");
                string confirmation = Console.ReadLine() ?? String.Empty;

                if (confirmation.ToLower() == "yes")
                {
                    long id = toDoLists.Count > 0 ? toDoLists[^1].Id + 1 : 1;
                    toDoLists.Add(new ToDoList{Id = id, Name = nameTask, Description = descriptionTask, DeadLine = deadlineTask, Priority = priorityTask, IsComplete = false});
                    
                    //Exit condition
                    isDone = true;
                    Console.WriteLine("================================================================================");
                    Console.WriteLine("[-] Successfully added now task to storage | Enter to continue");
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    Console.Clear();
                }
            }
        }

        private static void ViewTask(List<ToDoList> toDoLists)
        {
            Console.WriteLine("Choose the filter");
            Console.WriteLine("1. By A-Z");
            Console.WriteLine("2. By Date");
            Console.WriteLine("3. By Priority");
            Console.WriteLine("4. No Filter | Hit enter");
            Console.WriteLine("========================================================================================");
            Console.Write("Your action : ");

            string userInput = Console.ReadLine() ?? String.Empty;
            switch (userInput)
            {
                case "1":
                    Console.Clear();
                    IdFilter(toDoLists);
                    break;
                case "2":
                    Console.Clear();
                    DateFilter(toDoLists);
                    break;
                case "3":
                    Console.Clear();
                    PriorityFilter(toDoLists);
                    break;
                default:
                    Console.Clear();
                    RenderData(toDoLists);
                    break;
            }
            FocusedItem(toDoLists);
        }

        private static void MarkAsComplete(List<ToDoList> toDoLists)
        {
            RenderData(toDoLists);
            while (true)
            {
                Console.Write("Choose one of them for details || Exit : ");
                string userInput = Console.ReadLine() ?? String.Empty;
                try
                {
                    //Main Process
                    if (userInput.ToLower() == "exit" || String.IsNullOrEmpty(userInput)) break;
                    var item = toDoLists[Convert.ToInt32(userInput) - 1];
                    item.IsComplete = true;
                    
                    //Giving Feedback
                    Console.WriteLine("Successfully mark selected task to Complete | Enter to continue");
                    Console.ReadKey();
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("[!] Invalid Input | Enter to continue");
                }
            }
            Console.Clear();
        }

        private static void DeleteTask(List<ToDoList> toDoLists)
        {
            RenderData(toDoLists);
            while (true)
            {
                Console.Write("Choose one of them for details || Exit : ");
                string userInput = Console.ReadLine() ?? String.Empty;
                try
                {
                    //Main Process
                    if (userInput.ToLower() == "exit" || String.IsNullOrEmpty(userInput)) break;
                    toDoLists.RemoveAll(list => list.Id == Convert.ToInt32(userInput));
                    
                    //Giving Feedback
                    Console.WriteLine("Successfully deleted selected task | Enter to continue");
                    Console.ReadKey();
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("[!] Invalid Input | Enter to continue");
                }
            }
            Console.Clear();
        }
        
        //-------------------------------------------------Filter settings--------------------------------------------//
        private static void RenderData(List<ToDoList> toDoLists)
        {
            Console.WriteLine("========================================================================================");
            Console.WriteLine($"{"ID", -5} {"Name", -30} {"Deadline", -30} {"Priority", -10} Status  |");
            Console.WriteLine("----------------------------------------------------------------------------------------");
            foreach (var list in toDoLists)
            {
                var status = list.IsComplete ? "Done" : "On Going";
                var timeToDeadLine = list.DeadLine - DateTime.Now;

                if (list.IsComplete)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{list.Id, -5} {list.Name, -30} {list.DeadLine.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture), -30} {list.Priority, -10} {status}");
                    continue;
                }
                if (timeToDeadLine.TotalDays <= 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (timeToDeadLine.TotalDays <= 3)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                else if (timeToDeadLine.TotalDays <= 7)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                Console.WriteLine($"{list.Id, -5} {list.Name, -30} {list.DeadLine.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture), -30} {list.Priority, -10} {status}");
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("========================================================================================");
        }

        private static void IdFilter(List<ToDoList> toDoLists)
        {
            var filteredItems = toDoLists
                .Where(item => char.ToUpper(item.Name[0]) >= 'A' && char.ToUpper(item.Name[0]) <= 'Z').ToList();
            
            RenderData(filteredItems);
        }

        private static void DateFilter(List<ToDoList> toDoLists)
        {
            var filteredItems = toDoLists
                .Where(item => item.DeadLine >= DateTime.Today)
                .OrderBy(item => item.DeadLine)
                .ToList();
            
            RenderData(filteredItems);
        }

        private static void PriorityFilter(List<ToDoList> toDoLists)
        {
            Dictionary<string, int> priorityIndex = new Dictionary<string, int>
            {
                { "high", 3 },
                { "medium", 2 },
                { "low", 1 }
            };

            var filteredItems = toDoLists
                .Where(item => priorityIndex.ContainsKey(item.Priority.ToLower()))
                .OrderByDescending(item => priorityIndex[item.Priority.ToLower()])
                .ToList();
            
            RenderData(filteredItems);
        }

        private static void FocusedItem(List<ToDoList> toDoLists)
        {
            while (true)
            {
                Console.Write("Choose one of them for details || Exit : ");
                string userInput = Console.ReadLine() ?? String.Empty;

                try
                {
                    if (userInput.ToLower() == "exit" || string.IsNullOrEmpty(userInput)) break;
                    var item = toDoLists[Convert.ToInt32(userInput) - 1];
                    string status = item.IsComplete ? "Done" : "On going";
                    
                    Console.Clear();
                    Console.WriteLine("========================================================================================");
                    Console.WriteLine("ID : " + item.Id);
                    Console.WriteLine("Name : " + item.Name);
                    Console.WriteLine("Description : " + item.Description);
                    Console.WriteLine("Deadline : " + item.DeadLine.ToString("yyyy-MM-dd") + " | Today : " + DateTime.Today.ToString("yyyy-MM-dd"));
                    Console.WriteLine("Priority : " + item.Priority);
                    Console.WriteLine("Status : " + status);
                    Console.WriteLine("========================================================================================");
                    Console.WriteLine("Enter to continue");
                    Console.ReadKey();
                    break;

                }
                catch (Exception)
                {
                    Console.WriteLine("[!] Invalid Input | Enter to continue");
                }
            }
            Console.Clear();
        }

        public static void OnlyGoingTask(List<ToDoList> toDoLists)
        {
            var filteredItems = toDoLists
                .Where(item => item.IsComplete == false).ToList();
            
            RenderData(filteredItems);
        }
    }
}
