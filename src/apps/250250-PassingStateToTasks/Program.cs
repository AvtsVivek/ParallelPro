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

        // use an Action delegate and a named method
        Task task1 = new Task(new Action<object?>(PrintMessage!),
        "First task");
        
        // use a anonymous delegate
        Task task2 = new Task(delegate (object? obj) {
            PrintMessage(obj!);
        }, "Second Task");
        
        // use a lambda expression and a named method
        // note that parameters to a lambda don’t need
        // to be quoted if there is only one parameter
        Task task3 = new Task((obj) => PrintMessage(obj!), "Third task");
        
        // use a lambda expression and an anonymous method
        Task task4 = new Task((obj) => {
            PrintMessage(obj!);
        }, "Fourth task");

        task1.Start();
        task2.Start();
        task3.Start();
        task4.Start();

        // wait for input before exiting
        Console.WriteLine("Main method complete. Press enter to finish.");
        Console.ReadLine();
    }

    private static void PrintMessage(object message)
    {
        if (PrintThreadInfo)
        {
            var threadInfo = PrintThreadDetails("From PrintMessage Method");
            Console.WriteLine(threadInfo);
        }
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