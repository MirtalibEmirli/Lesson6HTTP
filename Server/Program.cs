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

                var url = context.Request.RawUrl;
                var u = url?.Split('/').ToList();
                //Console.WriteLine(url.Split('/').Last());

                var serviceName = $"Server.Services.{u[1]}Service";
                Assembly assembly = Assembly.GetExecutingAssembly();
                Type? type = assembly.GetType(serviceName);
                if (type is { })
                {
                    var service = Activator.CreateInstance(type) as UserService;
                    var methodName = url?.Split('/').Last();
                    var mi = type.GetMethod(methodName);
                    if (methodName=="GetAll")
                        mi?.Invoke(service, null);
                    else
                    {

                        var request = context.Request;
                        var stream = request.InputStream;
                        var bytes = new byte[1024];
                        var userData = stream.Read(bytes,0,bytes.Length);

                        var user = JsonSerializer.Deserialize<User>(Encoding.UTF8.GetString(bytes));
                   
                      //  mi?.Invoke(service,);
                    }
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
