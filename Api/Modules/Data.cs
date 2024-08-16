using OpenQA.Selenium;

namespace Synth.Api
{
    public static class Data
    {
        private static readonly string letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        private static readonly Random random = new();

        // methods
        public static string GetRandomString(int length)
        {
            char[] result = new char[length];
            for (int index = 0; index < length; index++)
                result[index] = letters[random.Next(letters.Length)];

            return new string(result);
        }

        public static int GetRandomNumber(int minimum, int maximum)
        {
            return random.Next(minimum, maximum + 1);
        }
    }
}
