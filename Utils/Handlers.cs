namespace Synth
{
    public static class Handlers
    {
        public static void SetupTerminationHandler()
        {
            static void OnProcessExit(object? _, EventArgs __)
            {
                Storage.currentRuntime?.Dispose();
                Api.Storage.driver?.Quit();
                Api.Storage.driver?.Dispose();
            }

            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
        }
    }
}
