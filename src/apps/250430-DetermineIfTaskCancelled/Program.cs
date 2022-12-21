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
        var cancellationTokenSource1 = new CancellationTokenSource();
        // create the cancellation token
        var cancellationToken1 = cancellationTokenSource1.Token;

        // create the first task, which we will let run fully
        var task1 = new Task(() =>
        {
            if (PrintThreadInfo)
            {
                var threadInfo = PrintThreadDetails("From Main Method");
                Console.WriteLine(threadInfo);
            }

            for (int i = 0; i < 10; i++)
            {
                cancellationToken1.ThrowIfCancellationRequested();
                Console.WriteLine("Task 1 - Int value {0}", i);
            }
        }, cancellationToken1);

        // create the second cancellation token source
        var cancellationTokenSource2 = new CancellationTokenSource();

        // create the cancellation token
        var cancellationToken2 = cancellationTokenSource2.Token;

        // create the second task, which we will cancel
        var task2 = new Task(() =>
        {
            if (PrintThreadInfo)
            {
                var threadInfo = PrintThreadDetails("From Main Method");
                Console.WriteLine(threadInfo);
            }

            for (int i = 0; i < int.MaxValue; i++)
            {
                cancellationToken2.ThrowIfCancellationRequested();
                Console.WriteLine("Task 2 - Int value {0}", i);
            }
        }, cancellationToken2);

        // start all of the tasks
        task1.Start();
        task2.Start();

        // cancel the second token source
        cancellationTokenSource2.Cancel();

        // write out the cancellation detail of each task
        Console.WriteLine("Task 1 cancelled? {0}", task1.IsCanceled);
        Console.WriteLine("Task 2 cancelled? {0}", task2.IsCanceled);

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
