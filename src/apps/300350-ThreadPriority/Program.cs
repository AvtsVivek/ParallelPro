using ThreadPriority;

Console.WriteLine("Hello, World!");

NetworkingWork.PrintThreadDetails();

var networkingWork = new NetworkingWork();

var bgThread1 = new Thread(networkingWork.CheckNetworkStatus!);
var bgThread2 = new Thread(networkingWork.CheckNetworkStatus!);
var bgThread3 = new Thread(networkingWork.CheckNetworkStatus!);
var bgThread4 = new Thread(networkingWork.CheckNetworkStatus!);
var bgThread5 = new Thread(networkingWork.CheckNetworkStatus!);

bgThread1.Priority = System.Threading.ThreadPriority.Lowest;
bgThread2.Priority = System.Threading.ThreadPriority.BelowNormal;
bgThread3.Priority = System.Threading.ThreadPriority.Normal;
bgThread4.Priority = System.Threading.ThreadPriority.AboveNormal;
bgThread5.Priority = System.Threading.ThreadPriority.Highest;

bgThread1.Start("Lowest");
bgThread2.Start("BelowNormal");
bgThread3.Start("Normal");
bgThread4.Start("AboveNormal");
bgThread5.Start("Highest");

for (int i = 0; i < 10; i++)
{
    Console.WriteLine("Main thread working...");
}

Console.WriteLine("Done");
Console.ReadKey();
