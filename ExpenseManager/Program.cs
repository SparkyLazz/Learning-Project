using Newtonsoft.Json;

namespace ExpenseManager
{
    internal static class Program
    {
        private static List<Expense> _expenses = new List<Expense>();
        private static string _appPath = "";
        private static string _jsonPath = "";
        private static void Main()
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
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Please choose an Valid options! | " + e.Message);
                throw;
            }
        }

        private static void AddExpense()
        {
            
        }

        private static void SetUp()
        {
            Console.WriteLine("Searching for SaveData . . .");
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            string updatedPath = Path.GetFullPath(Path.Combine(appPath, "../../../"));
            string[] json = Directory.GetFiles(updatedPath, "saveData.json");
            _jsonPath = json[0];
            _appPath = updatedPath;
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
        public required int Id { get; set; }
        public required string Description { get; set; }
        public required double Amount { get; set; }
        public required string Date { get; set; }
    }
}
