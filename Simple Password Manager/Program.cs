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
                    Console.WriteLine($"Error: {e.Message}");
                    Thread.Sleep(3000);
                    Console.Clear();
                }
            }
        }

        // Add new app credentials
        private static void AddAppSecure()
        {
            Console.Clear();
            Console.Write("Application Name | Empty = Apps : ");
            string applicationName = Console.ReadLine() ?? "Apps";
            Console.Write("Username | Empty = user : ");
            string applicationUsername = Console.ReadLine() ?? "user";
            Console.Write("Password | Empty = 123 : ");
            string applicationPassword = Console.ReadLine() ?? "123";

            Console.WriteLine("=========================================================");
            Console.WriteLine("Remember your KEY and IV! Forgetting them will make decryption impossible.");
            Console.WriteLine("=========================================================");
            Console.Write("Key | Minimum 16 bytes : ");
            byte[] key = Encoding.UTF8.GetBytes(Console.ReadLine() ?? string.Empty);
            Console.Write("IV | Minimum 16 bytes : ");
            byte[] iv = Encoding.UTF8.GetBytes(Console.ReadLine() ?? string.Empty);

            if (key.Length < 16 || iv.Length < 16)
            {
                Console.WriteLine("Key and IV must be at least 16 bytes.");
                return;
            }

            byte[] encryptedUsername = EncryptStringToBytes(applicationUsername, key, iv);
            byte[] encryptedPassword = EncryptStringToBytes(applicationPassword, key, iv);

            int id = _appSecures.Count > 0 ? _appSecures[^1].Id + 1 : 1;
            _appSecures.Add(new AppSecure
            {
                Id = id,
                ApplicationName = applicationName,
                Username = Convert.ToBase64String(encryptedUsername),
                Password = Convert.ToBase64String(encryptedPassword)
            });

            SaveFile(_jsonPath);
            Console.WriteLine("Successfully added new data to the database!");
            Console.ReadKey();
            Console.Clear();
        }

        // View app credentials
        private static void ViewAppSecure()
        {
            if (_appSecures.Count == 0)
            {
                Console.WriteLine("No data is stored!");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            RenderAppNames();
            Console.WriteLine("Enter ID, Key, and IV to decrypt:");

            try
            {
                Console.Write("ID: ");
                int userIndex = Convert.ToInt32(Console.ReadLine()) - 1;
                if (userIndex < 0 || userIndex >= _appSecures.Count)
                {
                    Console.WriteLine("Invalid ID.");
                    return;
                }

                Console.Write("Key: ");
                byte[] key = Encoding.UTF8.GetBytes(Console.ReadLine() ?? string.Empty);
                Console.Write("IV: ");
                byte[] iv = Encoding.UTF8.GetBytes(Console.ReadLine() ?? string.Empty);

                var item = _appSecures[userIndex];
                string decryptedUsername = DecryptStringFromBytes(Convert.FromBase64String(item.Username), key, iv);
                string decryptedPassword = DecryptStringFromBytes(Convert.FromBase64String(item.Password), key, iv);

                Console.WriteLine($"Application: {item.ApplicationName}");
                Console.WriteLine($"Username: {decryptedUsername}");
                Console.WriteLine($"Password: {decryptedPassword}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Delete app credentials
        private static void DeleteAppSecure()
        {
            if (_appSecures.Count == 0)
            {
                Console.WriteLine("No data is stored!");
                return;
            }

            RenderAppNames();
            Console.WriteLine("Enter ID, Key, and IV to delete:");

            try
            {
                Console.Write("ID: ");
                int userIndex = Convert.ToInt32(Console.ReadLine()) - 1;
                if (userIndex < 0 || userIndex >= _appSecures.Count)
                {
                    Console.WriteLine("Invalid ID.");
                    return;
                }

                Console.Write("Key: ");
                byte[] key = Encoding.UTF8.GetBytes(Console.ReadLine() ?? string.Empty);
                Console.Write("IV: ");
                byte[] iv = Encoding.UTF8.GetBytes(Console.ReadLine() ?? string.Empty);

                var item = _appSecures[userIndex];
                string decryptedUsername = DecryptStringFromBytes(Convert.FromBase64String(item.Username), key, iv);
                string decryptedPassword = DecryptStringFromBytes(Convert.FromBase64String(item.Password), key, iv);

                Console.WriteLine($"Decrypted Username: {decryptedUsername}");
                Console.WriteLine($"Decrypted Password: {decryptedPassword}");
                Console.Write("Confirm deletion (yes/no): ");
                if (Console.ReadLine()?.ToLower() == "yes")
                {
                    _appSecures.RemoveAt(userIndex);
                    SaveFile(_jsonPath);
                    Console.WriteLine("Application removed successfully!");
                }
                else
                {
                    Console.WriteLine("Deletion canceled.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Render application names
        private static void RenderAppNames()
        {
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
        }

        // Encrypt data
        static byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                using (var encryptor = aes.CreateEncryptor())
                {
                    byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                    return encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
                }
            }
        }

        // Decrypt data
        static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;
                    using (var decryptor = aes.CreateDecryptor())
                    {
                        byte[] decryptedBytes = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);
                        return Encoding.UTF8.GetString(decryptedBytes);
                    }
                }
            }
            catch
            {
                return "Invalid key or IV!";
            }
        }

        // Application setup
        private static void Setup()
        {
            Console.WriteLine("Getting everything ready");
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            string updatedPath = Path.GetFullPath(Path.Combine(appPath, "../../../"));
            string[] json = Directory.GetFiles(updatedPath, "*.json");
            _jsonPath = json[0];

            LoadFile(_jsonPath);
            Thread.Sleep(1000);
            Console.Clear();
        }

        // Save data to file
        private static void SaveFile(string filepath)
        {
            var json = JsonConvert.SerializeObject(_appSecures, Formatting.Indented);
            File.WriteAllText(filepath, json);
        }

        // Load data from file
        private static void LoadFile(string filepath)
        {
            if (File.Exists(filepath))
            {
                string json = File.ReadAllText(filepath);
                _appSecures = JsonConvert.DeserializeObject<List<AppSecure>>(json) ?? new List<AppSecure>();
            }
        }
    }

    // Data model
    public class AppSecure
    {
        public required int Id { get; set; }
        public required string ApplicationName { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
