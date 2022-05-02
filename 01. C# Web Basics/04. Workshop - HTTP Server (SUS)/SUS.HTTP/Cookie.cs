namespace SUS.HTTP
{
    public class Cookie
    {
        public Cookie(string cookieInfo)
        {
            var cookie = cookieInfo.Split("=", 2);
            this.Name = cookie[0];
            this.Value = cookie[1];
        }

        public string Name { get; set; }

        public string Value { get; set; }

        public override string ToString()
        {
            return $"{Name}:={Value}";
        }
    }
}