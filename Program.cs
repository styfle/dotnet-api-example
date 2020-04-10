using System;
using System.Net;
using System.Threading.Tasks;

namespace ConsoleApi
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var listener = new HttpListener();
            listener.Prefixes.Add("http://*:8080/");
            listener.Start();
            Console.WriteLine("Listening...");
            while (true) {
              var context = await listener.GetContextAsync();
              await Api.Hello.Handler(context.Request, context.Response);
            }
            //listener.Stop();
        }


    }
}

