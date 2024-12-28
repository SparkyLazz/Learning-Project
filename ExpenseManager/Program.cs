using Newtonsoft.Json;

namespace ExpenseManager
{
    internal static class Program
    {
        private static List<Expense> _expenses = new List<Expense>();
        private static string _jsonPath = "";
        private static void Main()
        {
            SetUp();
            while (true)
            {
                Console.WriteLine("Welcome to Expense Manager");
                Console.WriteLine("1. Add Expense");
                Console.WriteLine("2. View Expense");
                Console.WriteLine("3. Delete Expense");
                Console.WriteLine("4. Exit");
                Console.WriteLine("--------------------------");
                Console.WriteLine("Choose an option : ");
                try
                {
                    var options = Convert.ToInt16(Console.ReadLine());
                    switch (options)
                    {
                        case 1: AddExpense(); break;
                        case 2: ViewExpense(); break;
                        case 3: DeleteExpense(); break;
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Please choose an Valid options! | " + e.Message);
                    throw;
                }
            }
        }
        private static void AddExpense()
        {
            try
            {
                Console.WriteLine("-------------------------------------------------------");
                Console.WriteLine("Description : | Empty = item");
                string description = Console.ReadLine() ?? "item";
                Console.WriteLine("Amount : | Empty = 1");
                int amount = Convert.ToInt16(Console.ReadLine());
                Console.WriteLine("Date (YYYY-MM-DD): | Empty = Date.now");
                string date = Console.ReadLine() ?? DateTime.Now.ToString("yyyy-MM-dd");

                //Check if the count of the expense is more than 0 it will get the last item id + 1 , if not then it returns 1
                int id = _expenses.Count > 0 ? _expenses[^1].Id + 1 : 1;
                _expenses.Add(new Expense { Id = id, Description = description, Amount = amount, Date = date });
                
                //Dont forget to save to SaveData
                SaveFile(_jsonPath);
                Console.WriteLine("Expense added successfully!");
                Console.WriteLine("-------------------------------------------------------");
            }
            catch (Exception e)
            {
                Console.WriteLine("Something off when you trying to add Expense . . . | " + e.Message);
                throw;
            }
        }

        private static void ViewExpense()
        {
            Console.WriteLine($"{"ID",-5} {"Description",-20} {"Amount",-10} {"Date",-15}");
            Console.WriteLine(new string('-', 50));

            foreach (var expense in _expenses)
            {
                Console.WriteLine($"{expense.Id,-5} {expense.Description,-20} {expense.Amount,10:C} {expense.Date,-15}");
            }

        }

        private static void DeleteExpense()
        {
            
        }
        
        private static void SetUp()
        {
            //Searching directories
            Console.WriteLine("Searching for SaveData . . .");
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            string updatedPath = Path.GetFullPath(Path.Combine(appPath, "../../../"));
            string[] json = Directory.GetFiles(updatedPath, "saveData.json");
            
            //Stored Data
            _jsonPath = json[0];
            LoadExpenses(_jsonPath);
            
            //Give feedback
            Console.WriteLine("Loaded successfully . . .");
            Console.WriteLine("-------------------------------------------------------");
            Thread.Sleep(1000);
        }

        private static void SaveFile(string filepath)
        {
            var json = JsonConvert.SerializeObject(_expenses, Formatting.Indented);
            File.WriteAllText(filepath, json);
        }

        private static void LoadExpenses(string filepath)
        {
            if (File.Exists(filepath))
            {
                string json = File.ReadAllText(filepath);
                _expenses = JsonConvert.DeserializeObject<List<Expense>>(json) ?? new List<Expense>();
            }
        }
    }

    public class Expense
    {
        public required int Id;
        public required string Description;
        public required double Amount;
        public required string Date;
    }
}
