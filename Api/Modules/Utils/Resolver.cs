using OpenQA.Selenium;

namespace Synth.Api
{
    public static class Resolver
    {
        public static By GetIdentifier(string identifier, string method)
        {
            return method switch
            {
                "Id" => By.Id(identifier),
                "Name" => By.Name(identifier),
                "ClassName" => By.ClassName(identifier),
                "TagName" => By.TagName(identifier),
                "LinkText" => By.LinkText(identifier),
                "PartialLinkText" => By.PartialLinkText(identifier),
                "CssSelector" => By.CssSelector(identifier),
                "XPath" => By.XPath(identifier),
                _ => By.Id(identifier),
            };
        }
    }
}
