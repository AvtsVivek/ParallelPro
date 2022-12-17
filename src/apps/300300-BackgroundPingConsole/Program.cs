using System.Diagnostics;
using System.Net.NetworkInformation;

Console.WriteLine("Hello, World!");
PrintThreadDetails();

var bgThread = new Thread(() =>
{
    Console.WriteLine($"The thread id of the background thread is {Thread.CurrentThread.ManagedThreadId}");

    PrintThreadDetails();

    while (true)
    {
        bool isNetworkUp = NetworkInterface.GetIsNetworkAvailable();
        Console.WriteLine($"Is network available? Answer: {isNetworkUp}");
        Thread.Sleep(100);
    }
});

bgThread.IsBackground = true;
bgThread.Start();

for (int i = 0; i < 5; i++)
{
    Console.WriteLine("Main thread working...");
    // Task.Delay(500);
    Thread.Sleep(500);
}

Console.WriteLine("Exiting .... ");

// Console.WriteLine("Done");
// Console.ReadKey();

void PrintThreadDetails()
{
    Console.ForegroundColor= ConsoleColor.Green;

    Console.WriteLine($"The current thread id is {Thread.CurrentThread.ManagedThreadId}.");

    var backgroundString = Thread.CurrentThread.IsBackground ? "a background thread" : "NOT a background thread";

    Console.WriteLine($"This thread is {backgroundString}");

    var threadPoolInfoString = Thread.CurrentThread.IsThreadPoolThread ? "a thread pool thread" : "NOT a thread pool thread";

    Console.WriteLine($"This thread is {threadPoolInfoString}");

    Console.ForegroundColor= ConsoleColor.White;
}