using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace SimplePasswordManager
{
    internal static class Program
    {
        private static List<AppSecure> _appSecures = new List<AppSecure>();
        private static string _jsonPath = "";
        private static void Main()
        {
            Setup();
            while (true)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Welcome to Password Manager");
                Console.WriteLine("1. Add AppSecure");
                Console.WriteLine("2. View AppSecure");
                Console.WriteLine("3. Delete AppSecure");
                Console.WriteLine("=========================================================");
                Console.Write("Your action : ");

                try
                {
                    int userInput = Convert.ToInt16(Console.ReadLine());
                    switch (userInput)
                    {
                        case 1: AddAppSecure(); break;
                        case 2: ViewAppSecure(); break;
                        case 3: DeleteAppSecure(); break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Thread.Sleep(3000);
                    Console.Clear();
                    continue;
                }
            }
        }
        //|----------------------------------------------| Terminal Function |-------------------------------------------------------|
        private static void AddAppSecure()
        {
            Console.Clear();
            //Checking userInput
            Console.Write("Application Name | Empty = Apps : ");
            string applicationName = Console.ReadLine() ?? "Apps";
            Console.Write("Username | Empty = user : ");
            string applicationUsername = Console.ReadLine() ?? "user";
            Console.Write("Password | Empty = 123 : ");
            string applicationPassword = Console.ReadLine() ?? "123";
            
            //Making Key and IV
            Console.WriteLine("=========================================================");
            Console.WriteLine("Forgetting the KEY dan IV that mean it always return uncorrected return");
            Console.WriteLine("=========================================================");
            Console.Write("Key | Minimum 16 bytes : ");
            byte[] key = Encoding.UTF8.GetBytes(Console.ReadLine() ?? String.Empty);
            Console.Write("IV | Minimum 16 Bytes : ");
            byte[] iv = Encoding.UTF8.GetBytes(Console.ReadLine() ?? String.Empty);
            
            //Trying to Encrypt
            byte[] encryptedUsername = EncryptStringToBytes(applicationUsername, key, iv);
            byte[] encryptedPassword = EncryptStringToBytes(applicationPassword, key, iv);
            
            //Add into data and save
            _appSecures.Add(new AppSecure{ApplicationName = applicationName, Username = Convert.ToBase64String(encryptedUsername), Password = Convert.ToBase64String(encryptedPassword)});
            SaveFile(_jsonPath);
            
            Console.WriteLine("Successfully added new Data to database!");
            Console.ReadKey();
            Console.Clear();
        }

        private static void ViewAppSecure()
        {
            if (_appSecures.Count == 0)
            {
                Console.WriteLine("=========================================================");
                Console.WriteLine("No data is stored!");
                Console.WriteLine("=========================================================");
                Console.ReadKey();
                Console.Clear();
                return;
            }
            //Render the Stored Application Name
            int rowPerColumn = 5;
            int totalColumns = (_appSecures.Count + rowPerColumn - 1) / rowPerColumn;
            
            for (int row = 0; row < rowPerColumn; row++)
            {
                for (int col = 0; col < totalColumns; col++)
                {
                    int index = col * rowPerColumn + row;
                    if (index < _appSecures.Count)
                    {
                        Console.Write($"[{index + 1}] {_appSecures[index].ApplicationName, -10}");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("=========================================================");
            
            //Asking user which one Application
            Console.Write("ID : ");
            int userIndex = Convert.ToInt32(Console.ReadLine());
            Console.Write("Key | Minimum 16 bytes : ");
            byte[] key = Encoding.UTF8.GetBytes(Console.ReadLine() ?? String.Empty);
            Console.Write("IV | Minimum 16 Bytes : ");
            byte[] iv = Encoding.UTF8.GetBytes(Console.ReadLine() ?? String.Empty);
            Console.Clear();
            
            //The main Preview
            Console.WriteLine("=========================================================");
            Console.WriteLine("Remember! You have to remember the Key and IV");
            Console.WriteLine("You would loss the password if you forget the Key and IV");
            Console.WriteLine("=========================================================");
            
            //Trying Decrypt
            byte[] usernameCipherText = Convert.FromBase64String(_appSecures[userIndex - 1].Username);
            byte[] passwordCipherText = Convert.FromBase64String(_appSecures[userIndex - 1].Password);
            string decryptUsername = DecryptStringFromBytes(usernameCipherText, key, iv);
            string decryptPassword = DecryptStringFromBytes(passwordCipherText, key, iv);
            Thread.Sleep(5000);
            
            //Showing the password Information
            Console.WriteLine("Application Name : " + _appSecures[userIndex - 1].ApplicationName);
            Console.WriteLine("Username : " + decryptUsername);
            Console.WriteLine("Password : " + decryptPassword);
            Console.ReadKey();
            Console.Clear();
        }

        private static void DeleteAppSecure()
        {
            
        }
        
        //|------------------------------------------------| Crypt Function |--------------------------------------------------------|
        static byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
        {
            using (Aes aes = Aes.Create())
            using (var ms = new MemoryStream())
            using (var cs = new CryptoStream(ms, aes.CreateEncryptor(key, iv), CryptoStreamMode.Write))
            {
                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                cs.Write(plainBytes, 0, plainBytes.Length);
                cs.FlushFinalBlock();
                return ms.ToArray();
            }
        }

        static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            using (Aes aes = Aes.Create())
            using (MemoryStream ms = new MemoryStream(cipherText))
            using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(key, iv), CryptoStreamMode.Read))
            using (StreamReader reader = new StreamReader(cs))
            {
                return reader.ReadToEnd();
            }
        }
        //|---------------------------------------------| Application Function |-----------------------------------------------------|
        private static void Setup()
        {
            //Searching directories
            Console.WriteLine("Getting everything ready");
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            string updatedPath = Path.GetFullPath(Path.Combine(appPath, "../../../"));
            string[] json = Directory.GetFiles(updatedPath, "*.json");
            _jsonPath = json[0];
            
            //Load Data
            LoadFile(_jsonPath);
            Thread.Sleep(1000);
            Console.Clear();
        }
        
        private static void SaveFile(string filepath)
        {
            var json = JsonConvert.SerializeObject(_appSecures, Formatting.Indented);
            File.WriteAllText(filepath, json);
        }
        
        private static void LoadFile(string filepath)
        {
            if (File.Exists(filepath))
            {
                string json = File.ReadAllText(filepath);
                _appSecures = JsonConvert.DeserializeObject<List<AppSecure>>(json) ?? new List<AppSecure>();
            }
        }
    }

    public class AppSecure
    {
        public required string ApplicationName;
        public required string Username;
        public required string Password;
    }
}

