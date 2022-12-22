using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

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

        // Initialize the integer to the managed thread id of the
        // first thread that accesses the Value property.
        
        var number = new Lazy<int>(() => {
            if (PrintThreadInfo)
            {
                var threadInfo = PrintThreadDetails("From Main Method");
                Console.WriteLine(threadInfo);
            }
            return Thread.CurrentThread.ManagedThreadId; 
        });

        var t1 = new Thread(() => {
            if (PrintThreadInfo)
            {
                var threadInfo = PrintThreadDetails("From Main Method");
                Console.WriteLine(threadInfo);
            }
            Console.WriteLine("number on t1 = {0} ThreadID = {1}",
            number.Value, Thread.CurrentThread.ManagedThreadId);
        });

        t1.Start();

        var t2 = new Thread(() =>
        {
            if (PrintThreadInfo)
            {
                var threadInfo = PrintThreadDetails("From Main Method");
                Console.WriteLine(threadInfo);
            }
            Console.WriteLine("number on t2 = {0} ThreadID = {1}",
            number.Value, Thread.CurrentThread.ManagedThreadId);
        });
        
        t2.Start();

        var t3 = new Thread(() => 
        {
            if (PrintThreadInfo)
            {
                var threadInfo = PrintThreadDetails("From Main Method");
                Console.WriteLine(threadInfo);
            }
            Console.WriteLine("number on t3 = {0} ThreadID = {1}",
            number.Value, Thread.CurrentThread.ManagedThreadId);
        });
        
        t3.Start();

        // Ensure that thread IDs are not recycled if the
        // first thread completes before the last one starts.
        
        t1.Join();
        t2.Join();
        t3.Join();

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