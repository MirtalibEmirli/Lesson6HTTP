using Server.Services;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;
try
{
    await Task.Run(() =>
    {
        try
        {
            int port = 27001;
            var server = new HttpListener();
            server.Prefixes.Add($"http://localhost:{port}/");
            server.Start();
            Console.WriteLine($"Http server started on {port}");

            while (true)
            {
                var context = server.GetContext();
                var requestt = context.Request;
                var method = requestt.HttpMethod;
                var url = requestt.RawUrl;

                if (method == "POST" && url.Contains("/add"))
                {

                    using (var reader = new StreamReader(requestt.InputStream))
                    {
                        var user = JsonSerializer.Deserialize<User>(reader.ReadToEnd());
                        Console.WriteLine(user?.Name + "Posted");
                        UserService.Add(user);
                        using var response = context.Response;
                        var message = $"{user.Name} added";
                        var buffer = Encoding.UTF8.GetBytes(message);
                        response.ContentEncoding = Encoding.UTF8;
                        response.ContentLength64 = buffer.Length;
                        response.OutputStream.Write(buffer, 0, buffer.Length);
                    }
                }
                else if (requestt.HttpMethod == "GET" && url.Contains("/get"))
                {

                    var jsons = JsonSerializer.Serialize<List<User>>(UserService.GetAll());

                    using var response = context.Response;
                    var buffer = Encoding.UTF8.GetBytes(jsons);
                    response.ContentLength64 = buffer.Length;
                    response.ContentType = "application/json";
                    using (var stream = response.OutputStream)
                    {
                        stream.Write(buffer, 0, buffer.Length);
                    }
                    Console.WriteLine(jsons);
                }
                else if (requestt.HttpMethod == "DELETE"&&url.Contains("/delete")  )//

                {
                    var id = url.Split('/').Last();
                    if (int.TryParse(id, out int userid))
                    {
                        var succes = UserService.Delete(userid);
                        using var response = context.Response;
                        if (succes)
                        {
                            response.StatusCode = (int)HttpStatusCode.OK;
                            var message = Encoding.UTF8.GetBytes("User deleted");
                            response.ContentLength64 = message.Length;
                            response.OutputStream.Write(message, 0, message.Length);


                        }
                        else
                        {
                            response.StatusCode = (int)HttpStatusCode.NotFound;
                            var messsage = Encoding.UTF8.GetBytes("User not found");
                            response.ContentLength64 = messsage.Length;
                            response.OutputStream.Write(messsage, 0, messsage.Length);

                        }
                    }

                }
                else
                {
                    var data = File.ReadAllBytes("C:\\Users\\Mirtalib\\source\\repos\\Lesson6HTTP\\Server\\Views\\HTMLPage1.html");
                    context.Response.ContentType = "text/html";
                    context.Response.OutputStream.Write(data, 0, data.Length);

                    context.Response.Close();

                }



            }
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);
        }


    });






}
catch (Exception ex)
{

    Console.WriteLine(ex.Message);
}
