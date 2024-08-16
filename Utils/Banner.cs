using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Synth
{
    public static class Banner
    {
        private static readonly string[] banner =
        {
            "   ____            __   __ ",
            "  / __/__ __ ___  / /_ / / ",
            " _\\ \\ / // // _ \\/ __// _ \\",
            "/___/ \\_, //_//_/\\__//_//_/",
            "     /___/                 "
        };

        // methods
        public static void Display()
        {
            // display banner
            foreach (string part in banner)
            {
                for (int _ = 0; _ < ((Console.BufferWidth / 2) - (banner[0].Length / 2)); _++)
                    IO.Puts(" ", ConsoleColor.White, false);
                IO.Puts(part, ConsoleColor.DarkMagenta, true);
            }

            // display event
            DateTime today = DateTime.Now;

            int currentLeft = Console.CursorLeft;
            int currentTop = Console.CursorTop;

            Console.SetCursorPosition(48, currentTop - 1);
            switch ($"{today.Day}/{today.Month}")
            {
                case "1/1":
                    string newYearText = "Happy New Year!";

                    for (int index = 0; index < newYearText.Length; index += 5) {
                        IO.Puts($"{newYearText[index]}", ConsoleColor.Red, false);
                        IO.Puts($"{newYearText[index + 1]}", ConsoleColor.DarkYellow, false);
                        IO.Puts($"{newYearText[index + 2]}", ConsoleColor.Green, false);
                        IO.Puts($"{newYearText[index + 3]}", ConsoleColor.Blue, false);
                        IO.Puts($"{newYearText[index + 4]}", ConsoleColor.Cyan, false);
                    }
                    break;
                case "31/10":
                    string halloweenText = "Happy Halloween!";

                    for (int index = 0; index < halloweenText.Length; index += 2) {
                        IO.Puts($"{halloweenText[index]}", ConsoleColor.DarkYellow, false);
                        IO.Puts($"{halloweenText[index + 1]}", ConsoleColor.DarkGreen, false);
                    }
                    break;
                case "25/12":
                    string christmasText = "Happy Christmas!";
                    
                    for (int index = 0; index < christmasText.Length; index += 2) {
                        IO.Puts($"{christmasText[index]}", ConsoleColor.Red, false);
                        IO.Puts($"{christmasText[index + 1]}", ConsoleColor.White, false);
                    }
                    break;
            }
            Console.SetCursorPosition(currentLeft, currentTop);

            // make space
            IO.Puts("\n\n");
        }
    }
}
