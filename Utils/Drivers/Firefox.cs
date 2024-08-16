using Newtonsoft.Json;
using System.IO.Compression;

namespace Synth
{
    public static class Firefox
    {
        private static readonly string driverFilePath = Path.Combine(Paths.drivers, "geckodriver.exe");
        private static readonly string tempZipPath = Path.Combine(Paths.temp, "firefox.zip");

        // methods
        public static bool Exists()
        {
            return File.Exists(driverFilePath);
        }

        public static async Task<bool> Download(bool log = false)
        {
            if (log) IO.Puts("Firefox", ConsoleColor.DarkGray);

            // delete driver if it already exists
            if (File.Exists(driverFilePath))
            {
                if (log) IO.Puts(" - Deleting old driver . . .", ConsoleColor.DarkGray);
                File.Delete(driverFilePath);
            }

            // get the latest version
            if (log) IO.Puts(" - Getting latest driver version . . .", ConsoleColor.DarkGray);
            string versionUrl = "https://api.github.com/repos/mozilla/geckodriver/releases/latest";
            string latestVersion;

            using (HttpClient client = new())
            {
                client.DefaultRequestHeaders.UserAgent.ParseAdd("request");
                HttpResponseMessage versionResponse = await client.GetAsync(versionUrl);
                if (!versionResponse.IsSuccessStatusCode)
                    return false;

                string? jsonResponse = await versionResponse.Content.ReadAsStringAsync();
                dynamic json = JsonConvert.DeserializeObject(jsonResponse);
                latestVersion = json.tag_name.ToString();
            }

            string driverUrl = $"https://github.com/mozilla/geckodriver/releases/download/{latestVersion}/geckodriver-{latestVersion}-win64.zip";

            // download driver
            if (log) IO.Puts(" - Downloading driver . . .", ConsoleColor.DarkGray);
            using (HttpClient client = new())
            {
                HttpResponseMessage driverResponse = await client.GetAsync(driverUrl);
                if (!driverResponse.IsSuccessStatusCode)
                    return false;
                await File.WriteAllBytesAsync(tempZipPath, await driverResponse.Content.ReadAsByteArrayAsync());
            }

            // extract driver
            if (log) IO.Puts(" - Extracting driver . . .", ConsoleColor.DarkGray);
            ZipFile.ExtractToDirectory(tempZipPath, Paths.temp);

            // move and cleanup
            if (log) IO.Puts(" - Cleaning up . . .", ConsoleColor.DarkGray);
            string extractedDriverPath = Path.Combine(Paths.temp, "geckodriver.exe");
            if (File.Exists(extractedDriverPath))
                File.Move(extractedDriverPath, driverFilePath);

            File.Delete(tempZipPath);

            // status
            if (log) IO.Puts(" - Done!", ConsoleColor.DarkGray);
            return true;
        }
    }
}
