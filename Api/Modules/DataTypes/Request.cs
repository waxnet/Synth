using OpenQA.Selenium;

namespace Synth.Api
{
    public class Request(NetworkRequestSentEventArgs data)
    {
        private readonly NetworkRequestSentEventArgs _data = data;

        // properties
        public string? Id
        {
            get { return _data?.RequestId; }
        }
        public string? Url
        {
            get { return _data?.RequestUrl; }
        }
        public string? Method
        {
            get { return _data?.RequestMethod; }
        }
        public string? PostData
        {
            get { return _data?.RequestPostData; }
        }
        public IReadOnlyDictionary<string, string>? Headers
        {
            get { return _data?.RequestHeaders; }
        }
    }
}
