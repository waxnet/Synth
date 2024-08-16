namespace Synth.Api
{
    public static class InputOutput
    {
        public static readonly Dictionary<string, ConsoleColor> colors = new() {
            ["Black"] = ConsoleColor.Black,
            ["DarkBlue"] = ConsoleColor.DarkBlue,
            ["DarkGreen"] = ConsoleColor.DarkGreen,
            ["DarkCyan"] = ConsoleColor.DarkCyan,
            ["DarkRed"] = ConsoleColor.DarkRed,
            ["DarkMagenta"] = ConsoleColor.DarkMagenta,
            ["DarkYellow"] = ConsoleColor.DarkYellow,
            ["Gray"] = ConsoleColor.Gray,
            ["DarkGray"] = ConsoleColor.DarkGray,
            ["Blue"] = ConsoleColor.Blue,
            ["Green"] = ConsoleColor.Green,
            ["Cyan"] = ConsoleColor.Cyan,
            ["Red"] = ConsoleColor.Red,
            ["Magenta"] = ConsoleColor.Magenta,
            ["Yellow"] = ConsoleColor.Yellow,
            ["White"] = ConsoleColor.White
        };

        // methods
        public static void Print(string text, string color = "White")
        {
            Console.ForegroundColor = colors[color];
            Console.WriteLine(text);
            Console.ResetColor();
        }
        public static string Input(string text, string color = "White")
        {
            Console.ForegroundColor = colors[color];

            Console.Write(text);
            Console.CursorVisible = true;
            string? value = Console.ReadLine() ?? "";
            Console.CursorVisible = false;
            Console.ResetColor();

            return value;
        }
        public static void Clear()
        {
            Console.Clear();
        }
    }
}
