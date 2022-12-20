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

        // create the task
        var task1 = new Task<int>(() => {
            var sum = 0;

            if (PrintThreadInfo)
            {
                var threadInfo = PrintThreadDetails("From Task 1");
                Console.WriteLine(threadInfo);
            }

            for (var i = 0; i < 100; i++)
                sum += i;
            
            return sum;
        });

        // start the task
        task1.Start();

        // write out the result
        Console.WriteLine("Result 1: {0}", task1.Result);

        // create the task using state
        var task2 = new Task<int>(obj => {
            var sum = 0;
            var max = (int)obj!;

            if (PrintThreadInfo)
            {
                var threadInfo = PrintThreadDetails("From Task 2 ");
                Console.WriteLine(threadInfo);
            }

            for (var i = 0; i < max; i++)
                sum += i;
            
            return sum;

        }, 100);

        // start the task
        task2.Start();

        // write out the result
        Console.WriteLine("Result 2: {0}", task2.Result);

        // wait for input before exiting
        Console.WriteLine("Main method complete. Press enter to finish.");
        Console.ReadLine();
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
