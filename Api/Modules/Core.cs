using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace Synth.Api
{
    public static class Core
    {
        public static Driver? StartDriver(string browser, int width, int height, string userAgent = "")
        {
            if (Storage.driver != null)
                return null;

            if (browser.Equals("chrome", StringComparison.CurrentCultureIgnoreCase)) {
                // setup chrome options
                ChromeOptions options = new();
                options.AddArgument($"--window-size={width},{height}");
                options.AddArgument("--disable-search-engine-choice-screen");
                options.AddArgument("--disable-gpu");
                options.AddArgument("--disable-crash-reporter");
                options.AddArgument("--disable-extensions");
                options.AddArgument("--disable-in-process-stack-traces");
                options.AddArgument("--disable-logging");
                options.AddArgument("--disable-dev-shm-usage");
                options.AddArgument("--log-level=OFF");
                options.AddArgument("--output=/dev/null");

                options.AddExcludedArgument("enable-logging");

                options.AddUserProfilePreference("credentials_enable_service", false);
                options.AddUserProfilePreference("profile.password_manager_enabled", false);

                // user agent
                if (!string.IsNullOrEmpty(userAgent))
                    options.AddArgument($"user-agent={userAgent}");

                // initiate driver
                ChromeDriverService service = ChromeDriverService.CreateDefaultService(Paths.drivers);
                service.HideCommandPromptWindow = true;

                Storage.driver = new ChromeDriver(service, options);
                return new Driver(Storage.driver);
            }

            else if (browser.Equals("firefox", StringComparison.CurrentCultureIgnoreCase)) {
                // set logging level
                Environment.SetEnvironmentVariable("MOZ_LOG", "webdriver:0,mozprofile:0");

                // setup firefox options
                FirefoxOptions options = new();
                options.AddArgument($"--width={width}");
                options.AddArgument($"--height={height}");

                options.SetPreference("signon.storeWhenAutocompleteOff", false);
                options.SetPreference("devtools.console.stdout.content", false);
                options.SetPreference("logging.console.enabled", false);
                options.SetPreference("signon.autologin.proxy", false);
                options.SetPreference("signon.rememberSignons", false);
                options.SetPreference("signon.autofillForms", false);
                options.SetPreference("webdriver.log.file", "NUL");

                // user agent
                if (!string.IsNullOrEmpty(userAgent)) {
                    options.AddArgument($"--user-agent={userAgent}");
                }

                // initiate driver
                FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(Paths.drivers);
                service.SuppressInitialDiagnosticInformation = true;
                service.HideCommandPromptWindow = true;

                Storage.driver = new FirefoxDriver(service, options);
                return new Driver(Storage.driver);
            }

            return null;
        }
    }
}
