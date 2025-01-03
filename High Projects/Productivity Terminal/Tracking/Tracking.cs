namespace Productivity_Terminal.Tracking
{
    public class Tracking
    {
        public required long Id;
        public required string Name;
        public List<Record> Records = new List<Record>();

        public static void TrackingMenu(List<Tracking> tracks, List<ToDoList.ToDoList> toDoLists)
        {
            var isExit = false;
            while (!isExit)
            {
                Console.WriteLine("1. Start Tracking");
                Console.WriteLine("2. View Tracking");
                Console.WriteLine("3. Analyze Tracking");
                Console.WriteLine("4. Delete Tracking");
                Console.WriteLine("5. Back");
                Console.WriteLine("========================================================================================");
                
                Console.Write("Your action : ");
                string userInput = Console.ReadLine() ?? String.Empty;

                switch (userInput)
                {
                    case "1":
                        Console.Clear();
                        AddTrack(tracks, toDoLists);
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

        private static void AddTrack(List<Tracking> tracks, List<ToDoList.ToDoList> toDoLists)
        {
            ToDoList.ToDoList.OnlyGoingTask(toDoLists);
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
                    Console.WriteLine("Are you want track this task (yes / no) : ");
                    string input = Console.ReadLine() ?? string.Empty;
                    if(input.ToLower() == "no" || string.IsNullOrEmpty(input)) break;
                    
                    //Condition if yes
                    Track(tracks, item);
                    break;
                } 
                catch (Exception)
                {
                    Console.WriteLine("[!] Invalid Input | Enter to continue");
                }
            }
            Console.Clear();
        }

        private static void Track(List<Tracking> tracks, ToDoList.ToDoList item)
        {
            Console.Clear();
            Console.WriteLine("========================================================================================");
            Console.WriteLine("Name : " + item.Name);
            Console.WriteLine("Description : " + item.Description);
            Console.WriteLine("Deadline : " + item.DeadLine.ToString("yyyy-MM-dd") + " | Today : " + DateTime.Today.ToString("yyyy-MM-dd"));
            Console.WriteLine("Priority : " + item.Priority);
            Console.WriteLine("========================================================================================");
            Console.WriteLine("Press S to Start Tracking | It keeps updating every 100 milliseconds");
            
            var isRunning = false;
            var startTime = DateTime.MinValue;
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(intercept: true).Key;
                    if (key == ConsoleKey.S && !isRunning)
                    {
                        startTime = DateTime.Now;
                        isRunning = true;
                    }

                    if (key == ConsoleKey.T && isRunning)
                    {
                        var result = new Record
                        {
                            StartTime = startTime,
                            EndTime = DateTime.Now
                        };
                        
                        var trackingItem = tracks.FirstOrDefault(t => t.Id == item.Id);
                        if (trackingItem != null)
                        {
                            trackingItem.Records.Add(result);
                            break;
                        }
                        long id = tracks.Count > 0 ? tracks[^1].Id + 1 : 1;
                        tracks.Add(new Tracking{Id = id, Name = item.Name, Records = new List<Record>{ result }});
                        Console.Clear();
                        break;
                    }
                }
                if (isRunning)
                {
                    TimeSpan elapsedTime = DateTime.Now - startTime;
                    Console.Clear();
                    Console.WriteLine("========================================================================================");
                    Console.WriteLine("Name : " + item.Name);
                    Console.WriteLine("Deadline : " + item.DeadLine.ToString("yyyy-MM-dd") + " | Today : " + DateTime.Today.ToString("yyyy-MM-dd"));
                    Console.WriteLine($"Time spent: {elapsedTime.Hours:D2}:{elapsedTime.Minutes:D2}:{elapsedTime.Seconds:D2}:{elapsedTime.Milliseconds / 10:D2}");
                    Console.WriteLine("========================================================================================");
                    Console.WriteLine("[-] Press T to Stop the timer | It automatically saves in Local file");
                    Thread.Sleep(100);
                }
            }
            Console.Clear();
            Console.WriteLine("======================================= Result =======================================");
            Console.WriteLine("Project Name : " + tracks[^1].Name);
            Console.WriteLine("Start Time : " + tracks[^1].Records[^1].StartTime.ToString("yyyy-MM-dd : HH-mm-ss"));
            Console.WriteLine("End Time : " + tracks[^1].Records[^1].StartTime.ToString("yyyy-MM-dd : HH-mm-ss"));
            Console.WriteLine("Duration : " + tracks[^1].Records[^1].Duration.ToString(@"hh\:mm\:ss"));
            Console.WriteLine("======================================================================================");
            Console.WriteLine("Press Enter to continue");
            Console.ReadKey();
            Console.Clear();
        }
    }

    
    
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Record
    {
        public required DateTime StartTime { get; set; }
        public required DateTime EndTime { get; set; }
        public TimeSpan Duration => EndTime - StartTime;

    }
}

