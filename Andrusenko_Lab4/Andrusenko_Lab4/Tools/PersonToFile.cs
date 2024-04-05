using Andrusenko_Lab4.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace Andrusenko_Lab4.Tools
{
    public class PersonToFile
    {
        public static bool Save(ObservableCollection<Person> list)
        {
            string json = JsonSerializer.Serialize(list);
            File.WriteAllText("users.bin", json);
            
            return true;
        }
        
        public static ObservableCollection<Person> Get()
        {
            ObservableCollection<Person> list = new ObservableCollection<Person>();
            if (File.Exists("users.bin") && File.ReadAllText("users.bin") != "")
            {
                string json = File.ReadAllText("users.bin");
                list = JsonSerializer.Deserialize<ObservableCollection<Person>>(json);
            }

            return list;
        }
    }
}
