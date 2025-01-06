using Productivity_Terminal.Json;
// ReSharper disable UseCollectionExpression
// ReSharper disable NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract

namespace Productivity_Terminal
{
    internal static class Program
    {
        private static List<ToDoList.ToDoList> _toDoLists = new List<ToDoList.ToDoList>();
        private static List<Tracking.Tracking> _tracking = new List<Tracking.Tracking>();
        private static string[]? _jsonPaths;

        private static void Main()
        {
            Setup();
            MainMenu();
        }

        private static void Setup()
        {
            Console.Title = "Productivity Terminal";
            Console.WriteLine("Getting Everything ready");
            AppDomain.CurrentDomain.ProcessExit += OnApplicationQuit;
            
            // Load a bunch of data
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            string updatePath = Path.GetFullPath(Path.Combine(appPath, "../../../"));
            _jsonPaths = Directory.GetFiles(updatePath, "*.json");

            // Load to-do list from JSON
            ReloadData();
            Console.Clear();
        }

        private static void ReloadData()
        {
            // Reload to-do list from JSON
            _toDoLists = JsonManager.LoadFIle<List<ToDoList.ToDoList>>(_jsonPaths![0]) ?? new List<ToDoList.ToDoList>();
            _tracking = JsonManager.LoadFIle<List<Tracking.Tracking>>(_jsonPaths[1]) ?? new List<Tracking.Tracking>();
            Console.Clear();
        }

        private static void MainMenu()
        {
            bool isExit = false;
            while (!isExit)
            {
                Console.WriteLine("1. To do list");
                Console.WriteLine("2. Track Productivity");
                Console.WriteLine("3. Add routine");
                Console.WriteLine("3. Exit");
                Console.WriteLine("========================================================================================");
                Console.Write("Your action : ");

                // Handle user input
                string userInput = Console.ReadLine() ?? string.Empty;
                switch (userInput)
                {
                    case "1":
                        Console.Clear();
                        ReloadData();
                        ToDoList.ToDoList.ToDoListMenu(_toDoLists);
                        JsonManager.SaveFile(_jsonPaths![0], _toDoLists);
                        break;
                    case "2":
                        Console.Clear();
                        ReloadData();
                        Tracking.Tracking.TrackingMenu(_tracking, _toDoLists);
                        JsonManager.SaveFile(_jsonPaths![1], _tracking);
                        break;
                    case "3":
                        isExit = true;
                        break;
                    default:
                        Console.WriteLine("[!] Invalid Input | Enter to continue");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
            Environment.Exit(0);
        }

        private static void OnApplicationQuit(object? sender, EventArgs e)
        {
            Console.Clear();
            Console.WriteLine("Application is quitting. Saving data...");
            JsonManager.SaveFile(_jsonPaths![0], _toDoLists);
            JsonManager.SaveFile(_jsonPaths![1], _tracking);
            //Add more
            Console.WriteLine("Data saved successfully!");
            Thread.Sleep(2000);
        }
    }
}
