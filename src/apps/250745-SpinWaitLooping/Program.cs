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

        // create a cancellation token source
        var cancellationTokenSource = new CancellationTokenSource();

        var cancellationToken = cancellationTokenSource.Token;

        // create the first task
        var t1 = Task.Factory.StartNew(() => {
            if (PrintThreadInfo)
            {
                var threadInfo = PrintThreadDetails("From Main Method");
                Console.WriteLine(threadInfo);
            }
            Console.WriteLine("Task 1 waiting for cancellation");
            cancellationToken.WaitHandle.WaitOne();
            Console.WriteLine("Task 1 cancelled");
            cancellationToken.ThrowIfCancellationRequested();
        }, cancellationToken);

        // create the second task, which will use a code loop
        var t2 = Task.Factory.StartNew(() => {
            if (PrintThreadInfo)
            {
                var threadInfo = PrintThreadDetails("From Main Method");
                Console.WriteLine(threadInfo);
            }
            // enter a loop until t1 is cancelled
            while (!t1.Status.HasFlag(TaskStatus.Canceled))
            {
                // do nothing - this is a code loop
            }
            Console.WriteLine("Task 2 exited code loop");
        });

        // create the third loop which will use spin waiting
        var t3 = Task.Factory.StartNew(() => {
            if (PrintThreadInfo)
            {
                var threadInfo = PrintThreadDetails("From Main Method");
                Console.WriteLine(threadInfo);
            }
            // enter the spin wait loop
            while (t1.Status != TaskStatus.Canceled)
            {
                Thread.SpinWait(1000);
            }
            Console.WriteLine("Task 3 exited spin wait loop");
        });

        // prompt the user to hit enter to cancel
        Console.WriteLine("Press enter to cancel token");
        Console.ReadLine();
        cancellationTokenSource.Cancel();


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
