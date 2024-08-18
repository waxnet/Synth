using OpenQA.Selenium;

namespace Synth.Api
{
    public class Response(NetworkResponseReceivedEventArgs data)
    {
        private readonly NetworkResponseReceivedEventArgs _data = data;

        // properties
        public string? Id
        {
            get { return _data?.RequestId; }
        }
        public string? Url
        {
            get { return _data?.ResponseUrl; }
        }
        public long? StatusCode
        {
            get { return _data?.ResponseStatusCode; }
        }
        public string? Body
        {
            get { return _data?.ResponseBody; }
        }
        public string? Content
        {
            get { return _data?.ResponseContent?.ReadAsString(); }
        }
        public string? ResourceType
        {
            get { return _data?.ResponseResourceType; }
        }
        public IReadOnlyDictionary<string, string>? Headers
        {
            get { return _data?.ResponseHeaders; }
        }
    }
}
