using System.Globalization;
using Newtonsoft.Json;
namespace To_Do_List
{
    internal static class Program
    {
        private static List<DoTask> _tasks = new List<DoTask>();
        private static string _jsonPath = "";
        private static void Main()
        {
            Console.Title = "To do List App";
	        Console.WriteLine("Getting everything ready");
            _jsonPath = GetJsonFile();
            LoadFile(_jsonPath);
            Thread.Sleep(1000);
            Console.Clear();

            while (true)
            {
                //Render a Menu List
                Console.WriteLine("1. Add task");
                Console.WriteLine("2. View Task");
                Console.WriteLine("3. Mark as Complete");
                Console.WriteLine("4. Delete Task");
                Console.WriteLine("5. Exit");
                Console.WriteLine("========================================================================================");
                Console.Write("Your action : ");
                
                //Handle User action here
                string userInput = Console.ReadLine() ?? String.Empty;
                switch (userInput)
                {
                    case "1":
                        Console.Clear();
                        AddTask();
                        break;
                    case "2":
                        Console.Clear();
                        ViewTask();
                        break;
                    case "3":
                        Console.Clear();
                        MarkAsCompleteTask();
                        break;
                    case "4":
                        Console.Clear();
                        DeleteTask();
                        break;
                    case "5": 
                        Environment.Exit(1); break;
                    default: 
                        Console.WriteLine("[!] Invalid Input ");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }
        
        //-------------------------------------------------------- Main Settings -------------------------------------------------------------//
        private static void AddTask()
        {
            bool isDone = false;
            while (!isDone)
            {
                // Asking for information
                Console.Write("Task Name (default : Task): ");
                string nameTask = Console.ReadLine() ?? String.Empty;
                nameTask = string.IsNullOrEmpty(nameTask) ? "Task" : nameTask;

                Console.Write("Description (default : None) : ");
                string descriptionTask = Console.ReadLine() ?? String.Empty;
                descriptionTask = string.IsNullOrEmpty(descriptionTask) ? "None" : descriptionTask;

                Console.Write("DeadLine (default : Now | Format : YYYY-MM-DD ) : ");
                string deadLineInput = Console.ReadLine() ?? String.Empty;
                string deadLineTask = string.IsNullOrEmpty(deadLineInput) ? DateTime.Now.ToString("yyyy-MM-dd") : deadLineInput;

                Console.Write("Priority (High / Medium / Low | default : Medium ) : ");
                string priorityTask = Console.ReadLine() ?? String.Empty;
                priorityTask = string.IsNullOrEmpty(priorityTask) ? "Medium" : priorityTask;

                
                //Asking user for make sure everything correct
                Console.Write("Are you sure (yes / no | else = no) : ");
                string confirmation = Console.ReadLine() ?? "no";
                
                //If else statement
                if (confirmation.ToLower() == "yes")
                {
                    //Main Process
                    long id = _tasks.Count > 0 ? _tasks[^1].Id + 1 : 1;
                    _tasks.Add(new DoTask{ Id = id, Name = nameTask, Description = descriptionTask, Deadline = deadLineTask, Priority = priorityTask, IsComplete = false});
                    
                    //Saving changed
                    SaveFile(_jsonPath);
                    
                    //Exit condition
                    isDone = true;
                    Console.WriteLine("========================================================================================");
                    Console.WriteLine("[-] Successfully added now task to storage | Enter to continue");
                    Console.ReadKey();
                    Console.Clear();
                }
                Console.Clear();
            }
        }
        
        private static void ViewTask()
        {
            //render a Filter menu
            Console.WriteLine("Choose the filter");
            Console.WriteLine("1. By A-Z");
            Console.WriteLine("2. By Date");
            Console.WriteLine("3. By Priority");
            Console.WriteLine("4. No Filter | Hit enter");
            Console.WriteLine("========================================================================================");
            Console.Write("Your action : ");
            
            //Handle user action here
            string userInput = Console.ReadLine() ?? String.Empty;
            switch (userInput)
            {
                case "1":
                    Console.Clear();
                    IdFilter();
                    break;
                case "2":
                    Console.Clear();
                    DateFilter();
                    break;
                case "3":
                    Console.Clear();
                    PriorityFilter();
                    break;
                default:
                    Console.Clear();
                    NoFilter();
                    break;
            }
            FocusedSelection();
        }

        private static void MarkAsCompleteTask()
        {
            NoFilter();
            while (true)
            {
                Console.Write("Choose one of them for details || Exit : ");
                string userInput = Console.ReadLine() ?? String.Empty;
                try
                {
                    //Main Process
                    if (userInput.ToLower() == "exit" || String.IsNullOrEmpty(userInput)) break;
                    var item = _tasks[Convert.ToInt32(userInput) - 1];
                    item.IsComplete = true;
                    SaveFile(_jsonPath);
                    
                    //Giving Feedback
                    Console.WriteLine("Successfully mark selected task to Complete | Enter to continue");
                    Console.ReadKey();
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("[!] Invalid Input");
                }
            }
            Console.Clear();
        }

        private static void DeleteTask()
        {
            NoFilter();
            while (true)
            {
                Console.Write("Choose one of them for details || Exit : ");
                string userInput = Console.ReadLine() ?? String.Empty;
                try
                {
                    //Main process
                    if (userInput.ToLower() == "exit" || String.IsNullOrEmpty(userInput)) break;
                    
                    _tasks.RemoveAll(task => task.Id == Convert.ToInt32(userInput));
                    Console.WriteLine("Expense removed successfully! || Enter to continue");
                    Console.ReadKey();
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("[!] Invalid Input");
                }
            }
            Console.Clear();
        }

        //-------------------------------------------------------- Filter Settings -------------------------------------------------------------//
        private static void NoFilter()
        {
            Console.WriteLine("========================================================================================");
            Console.WriteLine($"{"ID", -5} {"Name", -30} {"Deadline", -30} {"Priority", -10} Status  |");
            Console.WriteLine("----------------------------------------------------------------------------------------");
            foreach (var task in _tasks)
            {
                string status = task.IsComplete ? "Done" : "On Going";
                //Render item using color
                TimeSpan timeToDeadLine = DateTime.ParseExact(task.Deadline, "yyyy-MM-dd", CultureInfo.CurrentCulture) - DateTime.Now;
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
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                Console.WriteLine($"{task.Id, -5} {task.Name, -30} {task.Deadline, -30} {task.Priority, -10} {status}");
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("========================================================================================");
        }

        private static void IdFilter()
        {
            var filteredItems = _tasks
                .Where(item => char.ToUpper(item.Name[0]) >= 'A' && char.ToUpper(item.Name[0]) <= 'Z').ToList();
            
            Console.WriteLine("========================================================================================");
            Console.WriteLine($"{"ID", -5} {"Name", -30} {"Deadline", -30} {"Priority", -10} Status  |");
            Console.WriteLine("----------------------------------------------------------------------------------------");
            foreach (var task in filteredItems)
            {
                string status = task.IsComplete ? "Done" : "On Going";
                //Render item using color
                TimeSpan timeToDeadLine = DateTime.ParseExact(task.Deadline, "yyyy-MM-dd", CultureInfo.CurrentCulture) - DateTime.Now;
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
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                Console.WriteLine($"{task.Id, -5} {task.Name, -30} {task.Deadline, -30} {task.Priority, -10} {status}");
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("========================================================================================");
        }

        private static void PriorityFilter()
        {
            Dictionary<string, int> priorityIndex = new Dictionary<string, int>
            {
                {"high", 3},
                {"medium", 2},
                {"low", 1}
            };
            
            var filteredItems = _tasks
                .Where(item => priorityIndex.ContainsKey(item.Priority.ToLower())) //Filter that is the priority of task exist in Dictionary key ? If yes than in continue to next line : If not skip
                .OrderByDescending(item => priorityIndex[item.Priority.ToLower()]) //Descending mean from end to start so High has 3 and Low has 1 Which mean High is first render and Low in the ned
                .ToList(); 
            
            Console.WriteLine("========================================================================================");
            Console.WriteLine($"{"ID", -5} {"Name", -30} {"Deadline", -30} {"Priority", -10} Status  |");
            Console.WriteLine("----------------------------------------------------------------------------------------");
            foreach (var task in filteredItems)
            {
                string status = task.IsComplete ? "Done" : "On Going";
                
                //Render item using color
                TimeSpan timeToDeadLine = DateTime.ParseExact(task.Deadline, "yyyy-MM-dd", CultureInfo.CurrentCulture) - DateTime.Now;
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
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                Console.WriteLine($"{task.Id, -5} {task.Name, -30} {task.Deadline, -30} {task.Priority, -10} {status}");
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("========================================================================================");
        }

        private static void DateFilter()
        {
            var filteredItems = _tasks
                .Where(item => DateTime.ParseExact(item.Deadline, "yyyy-MM-dd", CultureInfo.CurrentCulture) >= DateTime.Today)
                .OrderBy(item => item.Deadline)
                .ToList();
    
            Console.WriteLine("========================================================================================");
            Console.WriteLine($"{"ID", -5} {"Name", -30} {"Deadline", -30} {"Priority", -10} Status  |");
            Console.WriteLine("----------------------------------------------------------------------------------------");
            foreach (var task in filteredItems)
            {
                string status = task.IsComplete ? "Done" : "On Going";
                //Render item using color
                TimeSpan timeToDeadLine = DateTime.ParseExact(task.Deadline, "yyyy-MM-dd", CultureInfo.CurrentCulture) - DateTime.Now;
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
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                Console.WriteLine($"{task.Id, -5} {task.Name, -30} {task.Deadline, -30} {task.Priority, -10} {status}");
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("========================================================================================");
        }

        private static void FocusedSelection()
        {
            while (true)
            {
                Console.Write("Choose one of them for details || Exit : ");
                string userInput = Console.ReadLine() ?? String.Empty;
                try
                {
                    if (userInput.ToLower() == "exit" || String.IsNullOrEmpty(userInput)) break;
                    var item = _tasks[Convert.ToInt32(userInput) - 1];
                    string status = item.IsComplete ? "Done" : "On Going";
                    
                    Console.Clear();
                    Console.WriteLine("========================================================================================");
                    Console.WriteLine("ID : " + item.Id);
                    Console.WriteLine("Name : " + item.Name);
                    Console.WriteLine("Description : " + item.Description);
                    Console.WriteLine("Deadline : " + item.Deadline + " | Today : " + DateTime.Today.ToString("yyyy-MM-dd"));
                    Console.WriteLine("Priority : " + item.Priority);
                    Console.WriteLine("Status : " + status);
                    Console.WriteLine("========================================================================================");
                    Console.WriteLine("Enter to continue");
                    Console.ReadKey();
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("[!] Invalid Input");
                }
            }
            Console.Clear();
        }
        
        //-------------------------------------------------------- Json Settings -------------------------------------------------------------//
        private static string GetJsonFile()
        {
            //Searching Directories
            Console.WriteLine("[-] Searching for Data");
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            string updatePath = Path.GetFullPath(Path.Combine(appPath, "../../../"));
            string[] json = Directory.GetFiles(updatePath, "storage.json");
            
            //Stored Data
            return json[0];
        }
        
        private static void SaveFile(string jsonPath)
        {
            var json = JsonConvert.SerializeObject(_tasks, Formatting.Indented);
            File.WriteAllText(jsonPath, json);
        }
        
        private static void LoadFile(string jsonPath)
        {
            if (File.Exists(jsonPath))
            {
                string json = File.ReadAllText(jsonPath);
                _tasks = JsonConvert.DeserializeObject<List<DoTask>>(json) ?? new List<DoTask>();
            }
        }
        
    }
    
    public class DoTask
    {
        public required long Id;
        public required string Name;
        public required string Description;
        public required string Deadline;

        public required string Priority;
        public required bool IsComplete;
    }
}

