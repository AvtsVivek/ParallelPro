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

        // create the cancellation token sources
        var cancellationTokenSource1 = new CancellationTokenSource();
        var cancellationTokenSource2 = new CancellationTokenSource();
        var cancellationTokenSource3 = new CancellationTokenSource();

        // create a composite token source using multiple tokens
        var compositeCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
                cancellationTokenSource1.Token, 
                cancellationTokenSource2.Token, 
                cancellationTokenSource3.Token
                );

        // create a cancellable task using the composite token
        var task = new Task(() => {
            if (PrintThreadInfo)
            {
                var threadInfo = PrintThreadDetails("From Main Method");
                Console.WriteLine(threadInfo);
            }

            Console.WriteLine("Waiting for WaitOne");
            // wait until the token has been cancelled
            compositeCancellationTokenSource.Token.WaitHandle.WaitOne();
            
            Console.WriteLine("WaitOne done...");
            // throw a cancellation exception 
            throw new OperationCanceledException(compositeCancellationTokenSource.Token);
        }, compositeCancellationTokenSource.Token);


        // wait for input before we start the tasks
        Console.WriteLine("Press enter to start tasks");
        Console.WriteLine("Press enter again to cancel tasks");
        Console.ReadLine();

        // start the task
        task.Start();

        // read a line from the console.
        Console.ReadLine();

        // cancel the task
        Console.WriteLine("Cancelling tasks");

        // cancel one of the original tokens
        cancellationTokenSource2.Cancel();

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
