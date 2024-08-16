using OpenQA.Selenium.Chrome;
using System.IO.Compression;

namespace Synth
{
    public static class Chrome
    {
        private static readonly string driverFilePath = Path.Combine(Paths.drivers, "chromedriver.exe");
        private static readonly string tempZipPath = Path.Combine(Paths.temp, "chrome.zip");
        private static readonly string extractedFolderPath = Path.Combine(Paths.temp, "chromedriver-win64");

        // methods
        public static bool Exists()
        {
            return File.Exists(driverFilePath);
        }

        public static async Task<bool> Download(bool log = false)
        {
            if (log) IO.Puts("Chrome", ConsoleColor.DarkGray);

            // delete driver if it already exists
            if (File.Exists(driverFilePath))
            {
                if (log) IO.Puts(" - Deleting old driver . . .", ConsoleColor.DarkGray);
                File.Delete(driverFilePath);
            }

            // get latest version
            if (log) IO.Puts(" - Getting latest driver version . . .", ConsoleColor.DarkGray);
            string versionUrl = "https://googlechromelabs.github.io/chrome-for-testing/LATEST_RELEASE_STABLE";
            string latestVersion;

            using (HttpClient client = new())
            {
                HttpResponseMessage versionResponse = await client.GetAsync(versionUrl);
                if (!versionResponse.IsSuccessStatusCode)
                    return false;

                latestVersion = (await versionResponse.Content.ReadAsStringAsync()).Trim();
            }

            string driverUrl = $"https://storage.googleapis.com/chrome-for-testing-public/{latestVersion}/win64/chromedriver-win64.zip";

            // download driver
            if (log) IO.Puts(" - Downloading driver . . .", ConsoleColor.DarkGray);
            using (HttpClient client = new())
            {
                HttpResponseMessage driverResponse = await client.GetAsync(driverUrl);
                if (!driverResponse.IsSuccessStatusCode)
                    return false;

                // Save the downloaded content as a zip file
                await File.WriteAllBytesAsync(tempZipPath, await driverResponse.Content.ReadAsByteArrayAsync());
            }

            // extract driver
            if (log) IO.Puts(" - Extracting driver . . .", ConsoleColor.DarkGray);
            ZipFile.ExtractToDirectory(tempZipPath, Paths.temp);

            // move and cleanup
            if (log) IO.Puts(" - Cleaning up . . .", ConsoleColor.DarkGray);
            string extractedDriverPath = Path.Combine(extractedFolderPath, "chromedriver.exe");
            if (File.Exists(extractedDriverPath))
                File.Move(extractedDriverPath, driverFilePath);

            Directory.Delete(extractedFolderPath, true);
            File.Delete(tempZipPath);

            // status
            if (log) IO.Puts(" - Done!", ConsoleColor.DarkGray);
            return true;
        }
    }
}
