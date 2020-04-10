using System;
using System.Net;
using System.Threading.Tasks;

namespace ConsoleApi
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var port = 8080;
            var listener = new HttpListener();
            listener.Prefixes.Add($"http://*:{port}/");
            listener.Start();
            Console.WriteLine($"> Listening on http://localhost:{port}");
            var hello = new Api.Hello();
            while (true) {
              var context = await listener.GetContextAsync();
              await hello.Handler(context.Request, context.Response);
            }
            //listener.Stop();
        }


    }
}

