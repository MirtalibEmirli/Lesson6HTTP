using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Server.Services
{
    public class UserService
    {

        public List<User> GetAll()
        {
            var data = File.ReadAllText("C:\\Users\\Amirl_kl34\\source\\repos\\LEsson6HTTP\\Server\\MOCK_DATA (1).json");
            var users = JsonSerializer.Deserialize<List<User>>(data);
            Console.WriteLine(users[1].Name);
            return users;
        }

        public void Add(User u)
        {
            var data = File.ReadAllText("C:\\Users\\Amirl_kl34\\source\\repos\\LEsson6HTTP\\Server\\MOCK_DATA (1).json");
            var users = JsonSerializer.Deserialize<List<User>>(data);
            users.Add(u);
            var jsons = JsonSerializer.Serialize(users);
            File.WriteAllText("C:\\Users\\Amirl_kl34\\source\\repos\\LEsson6HTTP\\Server\\MOCK_DATA (1).json", jsons);
        }

        public void Delete(User u)
        {
            var data = File.ReadAllText("C:\\Users\\Amirl_kl34\\source\\repos\\LEsson6HTTP\\Server\\MOCK_DATA (1).json");
            var users = JsonSerializer.Deserialize<List<User>>(data);
            foreach (var item in users)
            {
                if (item == u)
                {
                    users.Remove(u); break;
                }
            }
            var jsons = JsonSerializer.Serialize(users);
            File.WriteAllText("C:\\Users\\Amirl_kl34\\source\\repos\\LEsson6HTTP\\Server\\MOCK_DATA (1).json", jsons);
        }
    }
}
