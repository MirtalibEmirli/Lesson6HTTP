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
        static string json = "C:\\Users\\Mirtalib\\source\\repos\\Lesson6HTTP\\Server\\MOCK_DATA (1).json";
        public static List<User> GetAll()
        {
            var data = File.ReadAllText(json);
            var users = JsonSerializer.Deserialize<List<User>>(data);

            return users;
        }

        public static void Add(User u)
        {
            try
            {
                var c = true;
                var data = File.ReadAllText(json);
                var users = JsonSerializer.Deserialize<List<User>>(data);

                foreach (var item in users)
                {
                    if (u.Id == item.Id)
                    {
                        users.Remove(item);
                        users.Add(u); c = true; break;
                    }
                }
                if (c)
                {
                    users.Add(u);
                }
                var jsons = JsonSerializer.Serialize(users);
                File.WriteAllText(json, jsons);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        public static bool Delete(int id)
        {
            var a = false;
            try
            {

                var data = File.ReadAllText(json);
                var users = JsonSerializer.Deserialize<List<User>>(data);
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
                File.WriteAllText(json, jsons);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return a;

        }
    }
}
