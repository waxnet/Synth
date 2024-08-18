using OpenQA.Selenium;

namespace Synth.Api
{
    public static class Resolver
    {
        public static By GetIdentifier(string identifier, string method)
        {
            return method.ToLower() switch
            {
                "id" => By.Id(identifier),
                "name" => By.Name(identifier),
                "classname" => By.ClassName(identifier),
                "tagname" => By.TagName(identifier),
                "linktext" => By.LinkText(identifier),
                "partiallinktext" => By.PartialLinkText(identifier),
                "cssselector" => By.CssSelector(identifier),
                "xpath" => By.XPath(identifier),
                _ => By.Id(identifier),
            };
        }
    }
}
