using NLua.Exceptions;
using Synth.Api;

namespace Synth
{
    public static class Program
    {
        public static void Main()
        {
            // setup console
            Window.SetSize(80, 20);
            Window.SetTitle(Info.title);
            Window.DisableResizing();
            Console.CursorVisible = false;

            // setup handlers
            Handlers.SetupTerminationHandler();

            // run setup
            Setup();

            while (true)
            {
                // display banner and options
                Console.Clear();
                Window.SetTitle(Info.title);
                Banner.Display();

                char selection = IO.WaitForInput(
                    "  [1] Run Script\n  [2] Update Drivers\n  [3] Exit\n\n",
                    ConsoleColor.DarkGray
                );

                // execute selected option
                Console.Clear();

                switch (selection)
                {
                    case '1':
                        RunScript();
                        break;
                    case '2':
                        Banner.Display();
                        UpdateDrivers();
                        break;
                    case '3':
                        return;
                }
            }
        }

        private static void Setup()
        {
            Console.Clear();
            Banner.Display();
            IO.Puts("Setup", ConsoleColor.DarkGray);

            if (!Paths.Exist()) {
                IO.Puts(" - Creating folders . . .", ConsoleColor.DarkGray);
                Paths.Create();
            }

            if (!Chrome.Exists()) {
                IO.Puts(" - Downloading Chrome driver . . .", ConsoleColor.DarkGray);
                Chrome.Download().Wait();
            }

            if (!Firefox.Exists()) {
                IO.Puts(" - Downloading Firefox driver . . .", ConsoleColor.DarkGray);
                Firefox.Download().Wait();
            }

            IO.Puts(" - Done!", ConsoleColor.DarkGray);
        }

        private static void RunScript()
        {
            string script = Files.AskForScript();
            if (script.Trim() == "")
                return;

            Storage.currentRuntime = Runtime.Create();
            try {
                Storage.currentRuntime?.DoFile(script);
            } catch (LuaScriptException error) {
                if (error.InnerException != null) {
                    IO.Puts($"\nInternal Exception: {error.InnerException.Message}", ConsoleColor.Red);
                } else {
                    IO.Puts($"\nException: {error.Message}", ConsoleColor.Red);
                }
            }
            Storage.currentRuntime?.Dispose();
            Api.Storage.driver?.Quit();
            Api.Storage.driver?.Dispose();
            Api.Storage.driver = null;

            IO.WaitForInput("\nScript execution ended, press any key to continue . . .", ConsoleColor.DarkGray);
        }

        private static void UpdateDrivers()
        {
            Task<bool> chromeTask = Chrome.Download(true);
            chromeTask.Wait();
            if (!chromeTask.Result)
                IO.Puts("Could not update Chrome driver.", ConsoleColor.Red);
            IO.NewLine();

            Task<bool> firefoxTask = Firefox.Download(true);
            firefoxTask.Wait();
            if (!firefoxTask.Result)
                IO.Puts("Could not update Firefox driver.", ConsoleColor.Red);

            IO.WaitForInput("\nPress any key to continue . . .", ConsoleColor.DarkGray);
        }
    }
}
