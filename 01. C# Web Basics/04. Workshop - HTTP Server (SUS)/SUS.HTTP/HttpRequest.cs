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

            this.Body = sb.ToString();
        }

        public string Path { get; set; }

        public string Method { get; set; }

        public List<Header> Headers { get; set; }

        public List<Cookie> Cookies { get; set; }

        public string Body { get; set; }
    }
}
