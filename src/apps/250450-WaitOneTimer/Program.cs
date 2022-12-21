using System.Threading.Tasks;
using System.Threading;

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

        // create the first task, which we will let run fully
        Task task1 = new Task(() => {
            if (PrintThreadInfo)
            {
                var threadInfo = PrintThreadDetails("From Main Method");
                Console.WriteLine(threadInfo);
            }

            for (int i = 0; i < Int32.MaxValue; i++)
            {
                // put the task to sleep for 3 seconds
                bool cancelled = cancellationToken.WaitHandle.WaitOne(3000);
                
                // print out a message
                Console.WriteLine("Task 1 - Int value {0}. Cancelled? {1}", i, cancelled);
                
                // check to see if we have been cancelled
                if (cancelled)
                    throw new OperationCanceledException(cancellationToken);
                
            }
        }, cancellationToken);

        // start task
        task1.Start();

        // wait for input before exiting
        Console.WriteLine("Press enter to cancel token.");
        Console.ReadLine();

        // cancel the token
        cancellationTokenSource.Cancel();

        // wait for input before exiting
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
