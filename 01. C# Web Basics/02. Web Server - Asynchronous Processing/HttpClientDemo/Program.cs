using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientDemo
{
    public class Program
    {
        const string NewLine = "\r\n";

        static async Task Main(string[] args)
        {
            TcpListener tcpListener = new TcpListener(
                IPAddress.Loopback, 80);
            
            tcpListener.Start();

            // deamon // service
            while (true)
            {
                var client = tcpListener.AcceptTcpClient();
                using (var stream = client.GetStream())
                {
                    byte[] buffer = new byte[1000000];
                    var length = stream.Read(buffer, 0, buffer.Length);

                    string requestString =
                        Encoding.UTF8.GetString(buffer, 0, length);

                    Console.WriteLine(requestString);

                    string html = $"<h1>Hello from StivanServer {DateTime.Now}</h1>";

                    string response = "HTTP/1.1 200 OK" + NewLine +
                        "Server: StivanServer 2020" + NewLine +
                        "Content-Type: text/html; charset=utf-8" + NewLine +
                        "Content-Length: " + html.Length + NewLine + NewLine +
                        html + NewLine;

                    byte[] responseBytes = Encoding.UTF8.GetBytes(response);

                    stream.Write(responseBytes);

                    Console.WriteLine(new string('-', 70));
                }
            }
        }


        // Reading data from internet
        public static async Task ReadData()
        {
            Console.OutputEncoding = Encoding.UTF8;

            string url = "https://softuni.bg/";
            HttpClient httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);

            //Console.WriteLine(html);

            var response = await httpClient.GetAsync(url);
            Console.WriteLine(response.Headers);
            Console.WriteLine(response.StatusCode);
        }
    }
}
