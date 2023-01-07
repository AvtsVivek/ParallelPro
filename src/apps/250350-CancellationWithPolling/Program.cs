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

        // create the cancellation token source
        var cancellationTokenSource = new CancellationTokenSource();

        // create the cancellation token
        var cancellationToken = cancellationTokenSource.Token;

        // create the task
        var task = new Task(() => {

            if (PrintThreadInfo)
            {
                var threadInfo = PrintThreadDetails("From Task Method");
                Console.WriteLine(threadInfo);
            }

            for (int i = 0; i < int.MaxValue; i++)
            {
                Thread.Sleep(100);
                if (cancellationToken.IsCancellationRequested)
                {
                    Console.WriteLine("Task cancel detected");
                    throw new OperationCanceledException(cancellationToken);
                }
                else
                    Console.WriteLine("Int value {0}", i);
                
            }
        }, cancellationToken);

        // wait for input before we start the task
        Console.WriteLine("Press enter to start task");
        Console.WriteLine("Press enter again to cancel task");
        Console.ReadLine();

        // start the task
        task.Start();

        // read a line from the console.
        Console.ReadLine();

        // cancel the task
        Console.WriteLine("Cancelling task");

        cancellationTokenSource.Cancel();

        // We cannot have the following wait here.
        // If we want to have this waits, then we need to use try catch block.
        // Thats for another example.

        //Task.WaitAll(task);
        //// or
        //task.Wait();

        // wait for input before exiting
        Console.WriteLine($"The status of the task is {task.Status}"); // Running, not cancled.
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
