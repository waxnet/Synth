namespace Synth.Api
{
    public static class Other
    {
        public static void SetScriptTitle(string title)
        {
            Synth.Window.SetTitle($"{Info.title} - {title}");
        }

        public static bool Wait(int seconds = 0)
        {
            Thread.Sleep(seconds * 1000);
            return true;
        }
    }
}
