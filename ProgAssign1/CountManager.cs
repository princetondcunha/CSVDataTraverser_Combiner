public class CountManager
{
    private static int processCount = 0;
    private static int skipCount = 0;

    public static void IncrementProcessCount()
    {
        processCount++;
    }

    public static int GetProcessCount()
    {
        return processCount;
    }

    public static void IncrementSkipCount()
    {
        skipCount++;
    }

    public static int GetSkipCount()
    {
        return skipCount;
    }
}