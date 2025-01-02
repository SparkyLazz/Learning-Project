using Newtonsoft.Json;

namespace Coding_Tracker
{
    internal static class Program
    {
        private static List<CodingSession> _codingSessions = new List<CodingSession>();
        private static string _jsonPath = "";
        private static void Main()
        {
            //Setting everything Ups
            _jsonPath = GetJsonFile();
            Console.WriteLine("[-] Searching json file in base directory");
            Console.WriteLine("[-] Loading the content");
            Thread.Sleep(1000);
            LoadFile(_jsonPath);
            Console.Clear();

            while (true)
            {
                //Render a Menu List
                Console.WriteLine("1. Start Track");
                Console.WriteLine("2. View Track");
                Console.WriteLine("3. Delete Track");
                Console.WriteLine("4. Analyze Track");
                Console.WriteLine("5. Exit");
                Console.WriteLine("========================================================================================");
                Console.Write("Your action : ");
                
                //Handle user Input
                string userInput = Console.ReadLine() ?? String.Empty;
                switch (userInput)
                {
                    case "1":
                        Console.Clear();
                        StartTracker();
                        break;
                    case "2":
                        Console.Clear();
                        ViewTracker();
                        break;
                    case "3":
                        Console.Clear();
                        DeleteTracker();
                        break;
                    case "4":
                        Console.Clear();
                        AnalyzeTrack();
                        break;
                    case "5":
                        Environment.Exit(1);
                        break;
                    default:
                        Console.WriteLine("[!] Invalid Input | Enter to continue");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }

            }
            
        }
        
        //---------------------------------- Main Settings  ----------------------------------//
        private static void StartTracker()
        {
            // Project Name ---------------------------------------------------------
            Console.Write("Project Name (Default: Project): ");
            string nameProject = Console.ReadLine() ?? string.Empty;
            nameProject = string.IsNullOrEmpty(nameProject) ? "Project" : nameProject;
            
            //Setup -----------------------------------------------------------------
            var isRunning = false;
            var startTime = DateTime.MinValue;
            Console.WriteLine("Press S to Start Tracking | It keeps updating every 100 milliseconds");
            
            //Main process ----------------------------------------------------------
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var userKey = Console.ReadKey(intercept: true).Key;
                    if (userKey == ConsoleKey.S && !isRunning)
                    {
                        startTime = DateTime.Now;
                        isRunning = true;   
                    }

                    if (userKey == ConsoleKey.T && isRunning)
                    {
                        long id = _codingSessions.Count > 0 ? _codingSessions[^1].Id + 1 : 1;
                        _codingSessions.Add(new CodingSession{Id = id, ProjectName = nameProject, StartTime = startTime, EndTime = DateTime.Now});
                        SaveFIle(_jsonPath);
                        
                        Console.Clear();
                        break;
                    }
                }
                //Main Preview ------------------------------------------------------
                if (isRunning)
                {
                    TimeSpan elapsedTime = DateTime.Now - startTime;
                    Console.Clear();  // Clear the console for updating the time
                    Console.WriteLine("========================================================================================");
                    Console.WriteLine("Project Name: " + nameProject);
                    Console.WriteLine($"Time spent: {elapsedTime.Hours:D2}:{elapsedTime.Minutes:D2}:{elapsedTime.Seconds:D2}:{elapsedTime.Milliseconds / 10:D2}");
                    Console.WriteLine("[-] Press T to Stop the timer | It automatically saves in Local file");
                    Thread.Sleep(100);  // Delay update every 100 milliseconds
                }
            }
            Console.WriteLine("======================================= Result =======================================");
            Console.WriteLine("Project Name : " + _codingSessions[^1].ProjectName);
            Console.WriteLine("Start Time : " + _codingSessions[^1].StartTime.ToString("yyyy-MM-dd : HH-mm-ss"));
            Console.WriteLine("End Time : " + _codingSessions[^1].EndTime.ToString("yyyy-MM-dd : HH-mm-ss"));
            Console.WriteLine("Duration : " + _codingSessions[^1].Duration.ToString(@"hh\:mm\:ss"));
            Console.WriteLine("======================================================================================");
            Console.WriteLine("Press Enter to continue");
            Console.ReadKey();
            Console.Clear();
        }

        private static void ViewTracker()
        {
            NoFilter();
            Console.WriteLine("Press Enter to continue");
            Console.ReadKey();
            Console.Clear();
        }

        private static void DeleteTracker()
        {
            NoFilter();
            while (true)
            {
                Console.Write("Choose ID || Exit : ");
                string userInput = Console.ReadLine() ?? String.Empty;
                try
                {
                    if (userInput.ToLower() == "exit" || String.IsNullOrEmpty(userInput)) break;
                    _codingSessions.RemoveAll(session => session.Id == Convert.ToInt32(userInput));
                    SaveFIle(_jsonPath);
                    Console.WriteLine("Expense removed successfully! || Enter to continue");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("[!] Invalid Input | Enter to continue");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            Console.Clear();
        }

        private static void AnalyzeTrack()
        {
            try
            {
                var topiD = _codingSessions.OrderByDescending(ts => ts.Duration).First();

                Console.WriteLine("=========================================================================================================================");
                Console.WriteLine($"{"ID",-5} {"Name",-30} {"Formatted Duration",-30} Duration (Graphical");
                Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------");

                foreach (var item in _codingSessions)
                {
                    // Calculate the graphical duration (scaled down by 5 minutes per '|')
                    int durationFormatted = item.EndTime > item.StartTime ? Convert.ToInt32(item.Duration.TotalMinutes / 5) : 0;
                    durationFormatted = durationFormatted < 1 ? 1 : durationFormatted;

                    // Format the duration in hh:mm:ss
                    string formattedDuration = item.Duration.ToString(@"hh\:mm\:ss");

                    Console.WriteLine($"{item.Id,-5} {item.ProjectName,-30} {formattedDuration,-30} {new string('|', durationFormatted)}" + (item.Id == topiD.Id ? "|     | The Longest! |" : ""));
                }
                Console.WriteLine("=========================================================================================================================");
                Console.WriteLine("Press Enter to continue");
                Console.ReadKey();
                Console.Clear();
            }
            catch (Exception e)
            {
                NoFilter();
                Console.WriteLine("Press Enter to continue | " + e.Message);
                Console.ReadKey();
                Console.Clear();
            }
        }
        //---------------------------------- Data Settings  ----------------------------------//
        private static void NoFilter()
        {
            Console.WriteLine("========================================================================================");
            Console.WriteLine($"{"ID",-5} {"Name",-30} {"Date",-30} {"Duration",-30}");
            Console.WriteLine("----------------------------------------------------------------------------------------");
            foreach (var item in _codingSessions)
            {
                string durationFormatted = item.EndTime > item.StartTime ? item.Duration.ToString(@"hh\:mm\:ss") : "Invalid Duration";

                Console.WriteLine($"{item.Id,-5} {item.ProjectName,-30} {item.StartTime:yyyy-MM-dd} {"", -19} {durationFormatted,-30}");
            }
            Console.WriteLine("========================================================================================");
        }
        //---------------------------------- Json Settings  ----------------------------------//
        private static string GetJsonFile()
        {
            Console.WriteLine("Getting Everything ready");
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            string updatePath = Path.GetFullPath(Path.Combine(appPath, "../../../"));
            string[] json = Directory.GetFiles(updatePath, "*.json");

            return json[0];
        }
        
        private static void SaveFIle(string jsonPath)
        {
            var json = JsonConvert.SerializeObject(_codingSessions, Formatting.Indented);
            File.WriteAllText(jsonPath, json);
        }

        private static void LoadFile(string jsonPath)
        {
            if (File.Exists(jsonPath))
            {
                string json = File.ReadAllText(jsonPath);
                _codingSessions = JsonConvert.DeserializeObject<List<CodingSession>>(json) ?? new List<CodingSession>();
            }
        }
    }

    public class CodingSession
    {
        public required long Id;
        public required string ProjectName;
        public DateTime StartTime;
        public DateTime EndTime;
        public TimeSpan Duration => EndTime - StartTime;
    }
}

