using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api
{
    public class Hello
    {
        private int Count = 0; // Example local state
        public async Task Handler(HttpListenerRequest request, HttpListenerResponse response)
        {
            Console.WriteLine($"{request.HttpMethod} {request.Url.PathAndQuery}");
            if (request.Url.AbsolutePath != "/favicon.ico") {
              Count++;
            }

            var data = new
            {
                count = Count,
                time = DateTime.Now,
                enable = true,
                nothing = request == null ? "nothing" : null,
                path = request.Url.AbsolutePath,
                port = request.Url.Port,
                query = request.QueryString,
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
