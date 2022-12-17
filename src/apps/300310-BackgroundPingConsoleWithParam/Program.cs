using System.Net.NetworkInformation;

Console.WriteLine("Hello, World!");

PrintThreadDetails();

var bgThread = new Thread((object? data) =>
{
    PrintThreadDetails();
    if (data is null) return;

    int counter = 0;
    var result = int.TryParse(data.ToString(), out int maxCount);
    if (!result) return;

    while (counter < maxCount)
    {
        bool isNetworkUp = NetworkInterface.GetIsNetworkAvailable();
        Console.WriteLine($"Is network available? Answer: {isNetworkUp}");
        Thread.Sleep(100);
        counter++;
    }
});

bgThread.IsBackground = true;
bgThread.Start(12);

for (int i = 0; i < 10; i++)
{
    Console.WriteLine("Main thread working...");
    Task.Delay(500);
}

Console.WriteLine("Done");
Console.ReadKey();

void PrintThreadDetails()
{
    
    Console.ForegroundColor = ConsoleColor.Green;

    Console.WriteLine($"The current thread id is {Thread.CurrentThread.ManagedThreadId}.");

    var backgroundString = Thread.CurrentThread.IsBackground ? "a background thread" : "NOT a background thread";

    Console.WriteLine($"This thread is {backgroundString}");

    var threadPoolInfoString = Thread.CurrentThread.IsThreadPoolThread ? "a thread pool thread" : "NOT a thread pool thread";

    Console.WriteLine($"This thread is {threadPoolInfoString}");
    
    Console.ForegroundColor = ConsoleColor.White;
}