using System;
using System.Net;
using System.Threading.Tasks;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConsoleApi
{
    class Program
    {
        private static int Count = 0;
        static async Task Main(string[] args)
        {
            var listener = new HttpListener();
            listener.Prefixes.Add("http://*:8080/");
            listener.Start();
            Console.WriteLine("Listening...");
            while (true) {
              var context = await listener.GetContextAsync();
              await Handler(context.Request, context.Response);
            }
            //listener.Stop();
        }

        async static Task Handler(HttpListenerRequest request, HttpListenerResponse response)
        {
            var nothing = request != null ? "nothing" : null;

            var data = new {
              count = (++Count),
              name = "styfle",
              time = DateTime.Now,
              enable = true,
              nothing = nothing,
            };

            var text = JsonSerializer.Serialize(data);
            var buffer = System.Text.Encoding.UTF8.GetBytes(text);
            response.ContentType = "application/json";
            response.ContentLength64 = buffer.Length;
            var output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
            response.Close();
        }
    }
}

