using System.Collections;
using Newtonsoft.Json;

namespace Productivity_Terminal.Json
{
    public static class JsonManager
    {
        public static void SaveFile<T>(string jsonPath, T content)
        {
            var json = JsonConvert.SerializeObject(content, Formatting.Indented);
            File.WriteAllText(jsonPath, json);
        }

        public static T LoadFIle<T>(string jsonPath)
        {
            if (File.Exists(jsonPath)) {
                
                string json = File.ReadAllText(jsonPath);
                try
                {
                    T? data = JsonConvert.DeserializeObject<T>(json);
                    if (data == null || data is ICollection { Count: 0 })
                    {
                        Console.WriteLine("Loaded Complete | The data is Empty");
                        return default!;
                    }
                    return data;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error loading file: {e.Message}"); 
                }
            }
            else
            {
                Console.WriteLine("File does not exist"); 
            }
            return default!;
        }
    }
}