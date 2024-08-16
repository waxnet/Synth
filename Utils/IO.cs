namespace Synth
{
    public static class IO
    {
        public static void Puts(string text, ConsoleColor color = ConsoleColor.White, bool newLine = true)
        {
            if (newLine)
                text += "\n";

            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        public static string Gets(string text, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;

            Console.Write(text);
            Console.CursorVisible = true;
            string? value = Console.ReadLine() ?? "";
            Console.CursorVisible = false;
            Console.ResetColor();

            return value;
        }

        public static char WaitForInput(string text = "", ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();

            return Console.ReadKey(true).KeyChar;
        }

        public static void NewLine()
        {
            Console.WriteLine();
        }
    }
}
