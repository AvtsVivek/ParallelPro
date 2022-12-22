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

        // define the function 
        var taskBody = new Func<string>(() => {
            if (PrintThreadInfo)
            {
                var threadInfo = PrintThreadDetails("From Main Method");
                Console.WriteLine(threadInfo);
            }
            Console.WriteLine("Task body working...");
            return "Task Result";
        });

        // create the lazy variable
        var lazyData = new Lazy<Task<string>>(() =>
            Task<string>.Factory.StartNew(taskBody));

        Console.WriteLine("Calling lazy variable");
        Console.WriteLine("Result from task: {0}", lazyData.Value.Result);

        // do the same thing in a single statement
        var lazyData2 = new Lazy<Task<string>>(
            () => Task<string>.Factory.StartNew(() => {
                if (PrintThreadInfo)
                {
                    var threadInfo = PrintThreadDetails("From Main Method");
                    Console.WriteLine(threadInfo);
                }
                Console.WriteLine("Task body working...");
                return "Task Result";
            }));

        Console.WriteLine("Calling second lazy variable");
        Console.WriteLine("Result from task: {0}", lazyData2.Value.Result);


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
