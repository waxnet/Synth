using OpenQA.Selenium;

namespace Synth.Api
{
    public class Element(IWebElement element)
    {
        private readonly IWebElement? _element = element;

        // properties
        public string? Text
        {
            get { return _element?.Text; }
            set { _element?.SendKeys(value); }
        }
        public string? TagName
        {
            get { return _element?.TagName; }
        }

        public bool? Enabled
        {
            get { return _element?.Enabled; }
        }
        public bool? Selected
        {
            get { return _element?.Selected; }
        }
        public bool? Displayed
        {
            get { return _element?.Displayed; }
        }
        public Vector2 Position
        {
            get { return new Vector2(_element?.Location.X, _element?.Location.Y); }
        }
        public Vector2 Size
        {
            get { return new Vector2(_element?.Size.Width, _element?.Size.Height); }
        }

        // methods
        public void Click()
        {
            _element?.Click();
        }
        public void Submit()
        {
            _element?.Submit();
        }

        public string? GetAttribute(string attributeName)
        {
            return _element?.GetAttribute(attributeName);
        }
        public string? GetCssValue(string propertyName)
        {
            return _element?.GetCssValue(propertyName);
        }
        public string? GetDomProperty(string propertyName)
        {
            return _element?.GetDomProperty(propertyName);
        }

        // custom
        public Element? FindElement(string identifier, string method = "Id")
        {
            By identifierMethod = Resolver.GetIdentifier(identifier, method);

            IWebElement? element = _element?.FindElement(identifierMethod);

            if (element == null)
                return null;
            return new Element(element);
        }
        public Element[]? FindElements(string identifier, string method = "Id")
        {
            By identifierMethod = Resolver.GetIdentifier(identifier, method);

            IWebElement[]? elements;
            try {
                elements = _element?.FindElements(identifierMethod).ToArray();
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
    }
}
