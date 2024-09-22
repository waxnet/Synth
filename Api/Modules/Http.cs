using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Synth.Api
{
    public static class Http
    {
        private static readonly HttpClient httpClient = new();
        private static readonly WebClient webClient = new();

        // methods
        public static string SendGetRequest(string url)
        {
            try {
                // make and check request
                var request = httpClient.GetAsync(url);
                request.Wait();
                request.Result.EnsureSuccessStatusCode();

                // return data
                var content = request.Result.Content.ReadAsStringAsync();
                content.Wait();

                return content.Result;
            } catch (Exception error) {
                return error.ToString();
            }
        }

        public static string SendPostRequest(string url, object data)
        {
            try {
                // convert data
                string json = JsonConvert.SerializeObject(data);
                StringContent jsonContent = new(json, Encoding.UTF8, "application/json");

                // make and check request
                var request = httpClient.PostAsync(url, jsonContent);
                request.Wait();
                request.Result.EnsureSuccessStatusCode();

                // return data
                var content = request.Result.Content.ReadAsStringAsync();
                content.Wait();

                return content.Result;
            } catch (Exception error) {
                return error.ToString();
            }
        }

        public static bool DownloadFile(string url, string filePath)
        {
            // make path safe
            string safePath = Path.Combine(Paths.output, filePath);

            // download file to path
            try {
                webClient.DownloadFile(new Uri(url), safePath);
                return true;
            } catch {
                return false;
            }
        }
    }
}
