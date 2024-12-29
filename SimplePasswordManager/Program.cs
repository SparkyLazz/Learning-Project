using System.Text;

namespace SimplePasswordManager
{
    internal static class Program
    {
        private static List<AppSecure> _appSecures = new List<AppSecure>();
        private static string _jsonPath = "";
        private static void Main()
        {
            Setup();
        }

        private static void Setup()
        {
            //Searching directories
            Console.WriteLine("Getting everything ready");
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            string updatedPath = Path.GetFullPath(Path.Combine(appPath, "../../../"));
            string[] json = Directory.GetFiles(updatedPath, "*.json");
            _jsonPath = json[0];
        }
        
        private static void SaveFile(string filepath)
        {
            //var json = JsonConvert.SerializeObject(_expenses, Formatting.Indented);
            //File.WriteAllText(filepath, json);
        }
        
        private static void LoadExpenses(string filepath)
        {
            //if (File.Exists(filepath))
            //{
            //    string json = File.ReadAllText(filepath);
            //    _expenses = JsonConvert.DeserializeObject<List<Expense>>(json) ?? new List<Expense>();
            //}
        }
    }

    public class AppSecure
    {
        public required string Username;
        public required string Password;
    }
}

