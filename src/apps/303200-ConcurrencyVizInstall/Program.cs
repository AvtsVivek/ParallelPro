using System.Diagnostics;
using System.Net.NetworkInformation;

Console.WriteLine("Hello, World!");

var bgThread = new Thread(() =>
{
    Console.WriteLine($"The thread id of the background thread is {Thread.CurrentThread.ManagedThreadId}");

    while (true)
    {
        bool isNetworkUp = NetworkInterface.GetIsNetworkAvailable();
        Console.WriteLine($"Is network available? Answer: {isNetworkUp}");
        Thread.Sleep(100);
    }
});

bgThread.IsBackground = true;
bgThread.Start();

var processThreads = Process.GetCurrentProcess().Threads;

foreach (ProcessThread thread in processThreads)
{
    Console.WriteLine(thread.Id);
    // Console.WriteLine();
}

for (int i = 0; i < 5; i++)
{
    Console.WriteLine("Main thread working...");
    // Task.Delay(500);
    Thread.Sleep(500);
}
Console.WriteLine($"The thread id of the main thread is {Thread.CurrentThread.ManagedThreadId}");
Console.WriteLine("Exiting .... ");

// Console.WriteLine("Done");
// Console.ReadKey();
