namespace ToDo_App_core.Helpers
{
    public static class TextFileHelper
    {
        public static void CreateTextFile(string fileName, string text)
        {
            var directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files");
            Directory.CreateDirectory(directoryPath);

            var filePath = Path.Combine(directoryPath, fileName);
            File.WriteAllText(filePath, text);
        }
    }
}
