using OpenQA.Selenium;
using System.Globalization;

namespace Synth.Api
{
    public class Cookies(ICookieJar? cookieJar)
    {
        private readonly ICookieJar? _cookieJar = cookieJar;

        // properties
        public Cookie[]? AllCookies
        {
            get
            {
                OpenQA.Selenium.Cookie[]? cookies = _cookieJar?.AllCookies.ToArray();
                if (cookies == null)
                    return null;

                Cookie[] wrappedCookies = new Cookie[cookies.Length];
                for (int index = 0; index < cookies.Length; index++)
                    wrappedCookies[index] = new Cookie(cookies[index]);

                return wrappedCookies;
            }
        }

        // methods
        public Cookie? GetCookie(string cookieName)
        {
            return new Cookie(_cookieJar?.GetCookieNamed(cookieName));
        }
        public void AddCookie(Cookie cookie)
        {
            _cookieJar?.AddCookie(ReverseCookie(cookie));
        }
        public void DeleteCookie(Cookie cookie)
        {
            _cookieJar?.DeleteCookie(ReverseCookie(cookie));
        }
        public void DeleteCookie(string cookieName)
        {
            _cookieJar?.DeleteCookieNamed(cookieName);
        }
        public void DeleteAllCookies()
        {
            _cookieJar?.DeleteAllCookies();
        }

        // interal methods
        private static OpenQA.Selenium.Cookie ReverseCookie(Cookie cookie)
        {
            return new OpenQA.Selenium.Cookie(
                cookie.Name ?? "",
                cookie.Value ?? "",
                cookie.Domain ?? "",
                cookie.Path ?? "",
                DateTime.ParseExact(cookie.Expiry ?? "", "dd/MM/yy-HH:mm:ss", CultureInfo.InvariantCulture),
                cookie.Secure ?? false,
                cookie.IsHttpOnly ?? false,
                cookie.SameSite ?? ""
            );
        }
    }
}
