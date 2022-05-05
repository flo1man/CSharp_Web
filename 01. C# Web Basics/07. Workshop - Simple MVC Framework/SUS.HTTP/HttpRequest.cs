using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SUS.HTTP
{
    public class HttpRequest
    {
        public HttpRequest(string requestString)
        {
            this.Headers = new List<Header>();
            this.Cookies = new List<Cookie>();
            this.FormData = new Dictionary<string, string>();

            var lines = requestString
                .Split(new string[] { HttpConstants.NewLine }
                , StringSplitOptions.None);

            var headerLine = lines[0];
            var headerLinesParts = headerLine.Split();
            this.Method = (HttpMethod)Enum.Parse(typeof(HttpMethod), headerLinesParts[0], true);
            this.Path = headerLinesParts[1];

            int lineIndex = 1;
            bool isInHeaders = true;
            StringBuilder sb = new StringBuilder();

            while (lineIndex < lines.Length)
            {
                var line = lines[lineIndex];
                lineIndex++;

                if (string.IsNullOrWhiteSpace(line))
                {
                    isInHeaders = false;
                    continue;
                }

                if (isInHeaders)
                {
                    this.Headers.Add(new Header(line));
                }
                else
                {
                    sb.AppendLine(line);
                }
            }

            if (this.Headers.Any(x => x.Name == HttpConstants.RequestCookieHeader))
            {
                var cookiesAsString =
                    this.Headers
                    .FirstOrDefault(x => x.Name == HttpConstants.RequestCookieHeader)
                    .Value;


                var extractCookies =
                    cookiesAsString.Split(new string[] { ": ", "; " }
                    , StringSplitOptions.RemoveEmptyEntries);

                foreach (var cookie in extractCookies)
                {
                    this.Cookies.Add(new Cookie(cookie));
                }
            }

            this.Body = sb.ToString();
            var parameters = this.Body.Split("&");
            foreach (var param in parameters)
            {
                var parameterParts = param.Split("=");
                var name = parameterParts[0];
                var value = parameterParts[1];
                if (!this.FormData.ContainsKey(name))
                {
                    this.FormData.Add(name, value);
                }
            }
        }

        public string Path { get; set; }

        public HttpMethod Method { get; set; }

        public ICollection<Header> Headers { get; set; }

        public ICollection<Cookie> Cookies { get; set; }

        public IDictionary<string, string> FormData { get; set; }

        public string Body { get; set; }
    }
}
