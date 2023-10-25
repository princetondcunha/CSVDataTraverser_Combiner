using System.Diagnostics;

using ProgAssign1;
class Program
{
    static void Main(string[] args)
    {
        Stopwatch stopwatch = new Stopwatch();

        Console.WriteLine("Hi!");

        //Get the Input Path
        string inputDirPath = DirectoryHandler.InputPath();
        //Start the timer
        stopwatch.Start();

        Console.Write("Traversing the Directories for CSV Files...");

        using (StreamWriter logFile = new StreamWriter(DirectoryHandler.getLogFilePath(), true))
        {
            TextWriter originalConsoleOut = Console.Out;
            Console.SetOut(logFile);
            Console.WriteLine("INFO:    Start of the Process");

            //Generate Headers
            ParseCSV.generateHeaders();

            //Traverse Directories
            string currDir = TraverseDirectories.setRootDirectory(inputDirPath);
            TraverseDirectories.TraverseDirectory(currDir);

            Console.WriteLine("INFO:    End of the Process");

            stopwatch.Stop();
            TimeSpan elapsed = stopwatch.Elapsed;
            Console.WriteLine("Total execution time: " + elapsed);
            Console.WriteLine("Total number of valid rows: " + CountManager.GetProcessCount());
            Console.WriteLine("Total number of skipped rows: " + CountManager.GetSkipCount());
            Console.SetOut(originalConsoleOut);
        }

        Console.WriteLine("All the CSV Files been processed.");
        Console.WriteLine("Location of Logs File: " + DirectoryHandler.logFilePath);
        Console.WriteLine("Location of the Output File: " + DirectoryHandler.outputFilePath);
        Console.WriteLine("Bye~");
    }
}