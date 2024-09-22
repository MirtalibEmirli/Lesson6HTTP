using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Server.Services
{
    public static class UserService
    {

        public static List<User> GetAll()
        {
            var data = File.ReadAllText("C:\\Users\\Mirtalib\\source\\repos\\Lesson6HTTP\\Server\\MOCK_DATA (1).json");
            var users = JsonSerializer.Deserialize<List<User>>(data);
            
            return users;
        }

        public static void  Add(User u)
        {
            var data = File.ReadAllText("C:\\Users\\Mirtalib\\source\\repos\\Lesson6HTTP\\Server\\MOCK_DATA (1).json");
            var users = JsonSerializer.Deserialize<List<User>>(data);
            users.Add(u);
            var jsons = JsonSerializer.Serialize(users);
            File.WriteAllText("C:\\Users\\Amirl_kl34\\source\\repos\\LEsson6HTTP\\Server\\MOCK_DATA (1).json", jsons);
        }

        public static bool Delete(int id)
        {
            var data = File.ReadAllText("C:\\Users\\Mirtalib\\source\\repos\\Lesson6HTTP\\Server\\MOCK_DATA (1).json");
            var users = JsonSerializer.Deserialize<List<User>>(data);
            var a = false;
            foreach (var item in users)
            {
                if (item.Id == id)
                {
                    users.Remove(item);
                    a = true;
                    break;
                }
            }
            var jsons = JsonSerializer.Serialize(users);
            File.WriteAllText("C:\\Users\\Amirl_kl34\\source\\repos\\LEsson6HTTP\\Server\\MOCK_DATA (1).json", jsons);
            return a;
        }
    }
}
