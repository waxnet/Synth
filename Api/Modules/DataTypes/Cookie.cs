namespace Synth.Api
{
    public class Cookie(OpenQA.Selenium.Cookie? cookie)
    {
        private readonly OpenQA.Selenium.Cookie? _cookie = cookie;

        // constructors
        public static Cookie New(string name, string value, string path = "", string domain = "")
        {
            return new Cookie(
                new OpenQA.Selenium.Cookie(
                    name,
                    value,
                    domain,
                    path,
                    DateTime.Today
                )
            );
        }
        public static Cookie NewSecure(string name, string value, string path, string domain, bool secure, bool isHttpOnly, string sameSite)
        {
            return new Cookie(
                new OpenQA.Selenium.Cookie(
                    name,
                    value,
                    domain,
                    path,
                    DateTime.Today,
                    secure,
                    isHttpOnly,
                    sameSite
                )
            );
        }

        // properties
        public string? Name
        {
            get { return _cookie?.Name; }
        }
        public string? Value
        {
            get { return _cookie?.Value; }
        }
        public string? Path
        {
            get { return _cookie?.Path; }
        }
        public string? Domain
        {
            get { return _cookie?.Domain; }
        }
        public string? Expiry
        {
            get { return _cookie?.Expiry?.ToString("dd/MM/yy-HH:mm:ss"); }
        }
        public bool? Secure
        {
            get { return _cookie?.Secure; }
        }
        public bool? IsHttpOnly
        {
            get { return _cookie?.IsHttpOnly; }
        }
        public string? SameSite
        {
            get { return _cookie?.SameSite; }
        }
    }
}
