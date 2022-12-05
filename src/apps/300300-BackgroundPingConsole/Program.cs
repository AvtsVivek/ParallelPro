using System.Net.NetworkInformation;

Console.WriteLine("Hello, World!");

var bgThread = new Thread(() =>
{
    while (true)
    {
        bool isNetworkUp = NetworkInterface.GetIsNetworkAvailable();
        Console.WriteLine($"Is network available? Answer: {isNetworkUp}");
        Thread.Sleep(100);
    }
});

bgThread.IsBackground = true;
bgThread.Start();

for (int i = 0; i < 10; i++)
{
    Console.WriteLine("Main thread working...");
    Task.Delay(500);
}

Console.WriteLine("Done");
Console.ReadKey();
