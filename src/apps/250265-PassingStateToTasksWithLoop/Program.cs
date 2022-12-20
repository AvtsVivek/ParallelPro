class Program
{
    private static bool PrintThreadInfo = false;

    private static void Main(string[] args)
    {

        if (args.Length == 1)
            bool.TryParse(args[0], out PrintThreadInfo);

        Console.WriteLine($"PrintThreadInfo var is {PrintThreadInfo}");

        if (PrintThreadInfo)
        {
            var threadInfo = PrintThreadDetails("From Main Method");
            Console.WriteLine(threadInfo);
        }

        string[] messages = { "First task", "Second task", "Third task", "Fourth task" };

        foreach (string msg in messages)
        {
            Task myTask = new Task(obj => PrintMessage((string)obj!), msg);
            myTask.Start();
        }

        // wait for input before exiting
        Console.WriteLine("Main method complete. Press enter to finish.");
        Console.ReadLine();
    }

    private static void PrintMessage(object message)
    {
        if (PrintThreadInfo)
        {
            var threadInfo = PrintThreadDetails($"From PrintMessage Method {message} ");
            Console.WriteLine(threadInfo);
        }
        else
            Console.WriteLine("Message: {0}", message);
    }

    private static string PrintThreadDetails(string contextInfo)
    {
        var finalString = string.Empty;

        finalString = finalString + Environment.NewLine;

        finalString = $"The context information is: {contextInfo}";

        finalString = finalString + Environment.NewLine + $"The current thread id is {Thread.CurrentThread.ManagedThreadId}.";

        var backgroundString = Thread.CurrentThread.IsBackground ? "a background thread" : "NOT a background thread";

        finalString = finalString + Environment.NewLine + $"This thread is {backgroundString}";

        var threadPoolInfoString = Thread.CurrentThread.IsThreadPoolThread ? "a thread pool thread" : "NOT a thread pool thread";

        finalString = finalString + Environment.NewLine + $"This thread is {threadPoolInfoString}";

        finalString = finalString + Environment.NewLine;

        return finalString;
    }
}