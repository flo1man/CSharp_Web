using System;
using System.Collections.Generic;
using System.Linq;
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

            var lines = requestString
                .Split(new string[] { HttpConstants.NewLine }
                , StringSplitOptions.None);

            var headerLine = lines[0];
            var headerLinesParts = headerLine.Split();
            this.Method = headerLinesParts[0];
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
        }

        public string Path { get; set; }

        public string Method { get; set; }

        public ICollection<Header> Headers { get; set; }

        public ICollection<Cookie> Cookies { get; set; }

        public string Body { get; set; }
    }
}
