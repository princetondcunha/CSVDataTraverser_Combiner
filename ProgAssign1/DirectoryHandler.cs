using System;
using System.IO;

namespace ProgAssign1
{
    public class DirectoryHandler
    {
        public static string logFilePath = "";
        public static string outputFilePath = "";
        public static string InputPath()
        {
            string? directoryPath = null;

            while (string.IsNullOrEmpty(directoryPath) || !Directory.Exists(directoryPath))
            {
                Console.Write("Enter the directory path to traverse: ");
                directoryPath = Console.ReadLine();

                if (string.IsNullOrEmpty(directoryPath))
                {
                    Console.WriteLine("You must provide a directory path.");
                }
                else if (!Directory.Exists(directoryPath))
                {
                    Console.WriteLine("The specified directory does not exist. Please provide a valid path.");
                }
            }

            string parentDirectory = Path.Combine(directoryPath, "..");
            string logDir = Path.Combine(parentDirectory, "logs");
            createDirectory(logDir);
            logFilePath = Path.Combine(logDir, "logs.txt");

            string outputDir = Path.Combine(parentDirectory, "Output");
            createDirectory(outputDir);
            outputFilePath = Path.Combine(outputDir, "output.csv");

            return directoryPath;
        }

        public static void createDirectory(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR:      An error occurred: {ex.Message}");
            }
        }

        public static string getLogFilePath()
        {
            return logFilePath;
        }
        public static string getOutputFilePath()
        {
            return outputFilePath;
        }
    }
}