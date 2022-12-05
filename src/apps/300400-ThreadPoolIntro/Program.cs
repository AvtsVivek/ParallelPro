using System.Net.NetworkInformation;

Console.WriteLine("Hello, World!");

ThreadPool.QueueUserWorkItem((obj) =>
{
    for (int i = 0; i < 20; i++)
    {
        bool isNetworkUp = NetworkInterface.GetIsNetworkAvailable();

        Console.WriteLine($"Is network available? Answer: { isNetworkUp}");

        Thread.Sleep(100);
    }
});

for (int i = 0; i < 10; i++)
{
    Console.WriteLine("Main thread working...");

    Task.Delay(500);
}

Console.WriteLine("Done");

Console.ReadKey();
