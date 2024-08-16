namespace Synth.Api
{
    public static class FileSystem
    {
        // files
        public static void WriteToFile(string filePath, string data)
        {
            string safePath = Path.Combine(Paths.output, filePath);
            if (!File.Exists(safePath))
                using (FileStream _ = File.Create(safePath)) { }
            File.WriteAllText(safePath, data);
        }
        public static void AppendToFile(string filePath, string data)
        {
            string safePath = Path.Combine(Paths.output, filePath);
            if (!File.Exists(safePath))
                using (FileStream _ = File.Create(safePath)) { }
            File.AppendAllText(safePath, data);
        }
        public static string ReadFromFile(string filePath)
        {
            string safePath = Path.Combine(Paths.output, filePath);

            if (!File.Exists(safePath))
                return "";
            return File.ReadAllText(safePath);
        }
        public static void DeleteFile(string filePath)
        {
            string safePath = Path.Combine(Paths.output, filePath);

            if (File.Exists(safePath))
                File.Delete(safePath);
        }

        // directories
        public static void CreateFolder(string folderPath)
        {
            string safePath = Path.Combine(Paths.output, folderPath);
            Directory.CreateDirectory(safePath);
        }
        public static void DeleteFolder(string folderPath)
        {
            string safePath = Path.Combine(Paths.output, folderPath);
            Directory.Delete(safePath, true);
        }

        // checks
        public static bool DoesFileExist(string filePath)
        {
            string safePath = Path.Combine(Paths.output, filePath);
            return File.Exists(safePath);
        }
        public static bool DoesFolderExist(string folderPath)
        {
            string safePath = Path.Combine(Paths.output, folderPath);
            return Directory.Exists(safePath);
        }
    }
}
