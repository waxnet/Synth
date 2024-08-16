namespace Synth
{
    public static class Paths
    {
        public static readonly string drivers = "bin/drivers";
        public static readonly string temp = "bin/temp";

        public static readonly string output = $"{AppContext.BaseDirectory}/workspace";

        // methods
        public static void Create()
        {
            // system
            if (!Directory.Exists(drivers))
                Directory.CreateDirectory(drivers);
            if (!Directory.Exists(temp))
                Directory.CreateDirectory(temp);

            // other
            if (!Directory.Exists(output))
                Directory.CreateDirectory(output);
        }

        public static bool Exist()
        {
            return (
                Directory.Exists(drivers) &&
                Directory.Exists(temp) &&

                Directory.Exists(output)
            );
        }
    }
}
