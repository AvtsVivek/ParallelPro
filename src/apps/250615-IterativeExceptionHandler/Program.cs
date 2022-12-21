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

        // create the cancellation token source and the token
        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        // create a task that waits on the cancellation token
        var task1 = new Task(() => {
            // wait forever or until the token is cancelled
            cancellationToken.WaitHandle.WaitOne(-1);
            // throw an exception to acknowledge the cancellation
            throw new OperationCanceledException(cancellationToken);
        }, cancellationToken);

        // create a task that throws an exceptiono
        var task2 = new Task(() => {
            throw new NullReferenceException();
        });

        // start the tasks
        task1.Start(); task2.Start();

        // cancel the token
        cancellationTokenSource.Cancel();

        // wait on the tasks and catch any exceptions
        try
        {
            Task.WaitAll(task1, task2);
        }
        catch (AggregateException ex)
        {
            // iterate through the inner exceptions using 
            // the handle method
            ex.Handle((inner) => {
                if (inner is OperationCanceledException)
                {
                    // ...handle task cancellation...
                    return true;
                }
                else
                {
                    // this is an exception we don't know how
                    // to handle, so return false
                    return false;
                }
            });
        }

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
