using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DemoExercise
{
    public class ChitankaDataGatherer
    {
        public List<RawProperty> GatherData(int maxId)
        {
            var properties = new List<RawProperty>();

            string regex = @"[А-Яа-я]\—";

            for (int i = 1; i <= maxId; i++)
            {
                var html = GetHtml(i).Result;

                // Indexes of start / end
                var start = html.IndexOf("<title>");
                var end = html.IndexOf("— Моята библиотека");
                var result = html
                    .Substring(start + 7, end - start - 8)
                    .Replace("\n", "")
                    .Replace("\r", "")
                    .Replace("\t", "");

                string author = String.Empty;
                string title = String.Empty;

                Match match = Regex.Match(result, regex);

                if (match.Success)
                {
                    var regexValue = match.Value;
                    var index = result.IndexOf(regexValue) + 2;
                    author = result.Substring(0, index - 1).Trim();
                    title = result.Substring(index).Trim();

                }
                else
                {
                    var lastDash = result.LastIndexOf(" — ");

                    if (lastDash < 0)
                    {
                        author = result.Trim();
                    }
                    else
                    {
                        author = result.Substring(0, lastDash).Trim();
                        title = result.Substring(lastDash + 2).Trim();
                    }
                }
                properties.Add(new RawProperty(author, title));
            }

            return properties;
        }
        public static async Task<string> GetHtml(int index)
        {
            HttpClient httpClient = new HttpClient();
            var url = $"https://chitanka.info/book/{index}";
            var bookInfo = await httpClient.GetStringAsync(url);
            return bookInfo;
        }
    }

    public class RawProperty
    {
        public RawProperty(string author, string title)
        {
            Author = author;
            Title = title;
        }

        public string Author { get; set; }

        public string Title { get; set; }
    }
}
