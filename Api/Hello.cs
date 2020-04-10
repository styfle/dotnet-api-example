using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api
{
    public class Hello
    {

        private static int Count = 0; // Example local state

        public async static Task Handler(HttpListenerRequest request, HttpListenerResponse response)
        {
            var nothing = request != null ? "nothing" : null;

            var data = new
            {
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
