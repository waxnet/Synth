using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace Synth.Api
{
    public class Driver(IWebDriver driver)
    {
        private readonly IWebDriver _driver = driver;

        // properties
        public string? Title
        {
            get { return _driver?.Title; }
        }
        public string? Url
        {
            get { return _driver?.Url; }
        }
        public string? PageSource
        {
            get { return _driver?.PageSource; }
        }
        public Window? Window
        {
            get
            {
                if (_driver?.Manage()?.Window == null)
                    return null;
                return new Window(_driver?.Manage()?.Window);
            }
        }
        public Network? Network
        {
            get
            {
                if (_driver?.Manage()?.Network == null)
                    return null;
                return new Network(_driver?.Manage().Network);
            }
        }
        public Cookies? Cookies
        {
            get
            {
                if (_driver?.Manage()?.Cookies == null)
                    return null;
                return new Cookies(_driver?.Manage().Cookies);
            }
        }

        // methods
        public void Quit()
        {
            _driver?.Quit();
        }
        public void Browse(string url)
        {
            _driver?.Navigate().GoToUrl(url);
        }
        public void Forward()
        {
            _driver?.Navigate().Forward();
        }
        public void Back()
        {
            _driver?.Navigate().Back();
        }
        public void Refresh()
        {
            _driver?.Navigate().Refresh();
        }

        // custom
        public Element? FindElement(string identifier, string method = "Id")
        {
            By identifierMethod = Resolver.GetIdentifier(identifier, method);

            IWebElement? element;
            try {
                element = _driver?.FindElement(identifierMethod);
            } catch {
                return null;
            }
            
            if (element == null)
                return null;
            return new Element(element);
        }
        public Element[]? FindElements(string identifier, string method = "Id")
        {
            By identifierMethod = Resolver.GetIdentifier(identifier, method);

            IWebElement[]? elements;
            try {
                elements = _driver?.FindElements(identifierMethod).ToArray();
            } catch {
                return null;
            }

            if (elements == null)
                return null;

            Element[] wrappedElements = new Element[elements.Length];
            for (int index = 0; index < elements.Length; index++)
                wrappedElements[index] = new Element(elements[index]);

            return wrappedElements;
        }
        public Element? WaitForElement(int timeout, string identifier, string method = "Id")
        {
            try {
                By identifierMethod = Resolver.GetIdentifier(identifier, method);

                WebDriverWait wait = new(_driver, TimeSpan.FromSeconds(timeout));
                IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(identifierMethod));

                return new Element(element);
            } catch (WebDriverTimeoutException) {
                return null;
            } catch (Exception) {
                return null;
            }
        }
    }
}
