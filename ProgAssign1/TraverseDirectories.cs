namespace ProgAssign1
{
    class TraverseDirectories
    {
        public static void TraverseDirectory(string directory)
        {
            //Get Subdirectories under the directory
            string[] subDirectories = Directory.GetDirectories(directory);
            if (subDirectories.Length > 0)
            {
                foreach (string subDirectory in subDirectories)
                {
                    Console.WriteLine("INFO:    Directory changed to "+subDirectory); //Use this for debugging
                    TraverseDirectory(subDirectory);
                }
            }

            GetDirectoryFiles(directory);
        }

        public static void GetDirectoryFiles(string directory)
        {
            // Get files in the directory
            string[] files = Directory.GetFiles(directory);
            foreach (string file in files)
            {
                if (Path.GetExtension(file).Equals(".csv", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("INFO:    Parsing CSV File " + file);
                    ParseCSV.readCombineCSV(file);
                }
            }
        }

        public static string setRootDirectory(string dirPath)
        {
            // Set Current Directory
            Directory.SetCurrentDirectory(dirPath);
            string currentDirectory = Directory.GetCurrentDirectory();
            Console.WriteLine("INFO:    Directory changed to " + currentDirectory);
            return currentDirectory;
        }
    }
}
