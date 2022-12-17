namespace ThreadPriority
{
    internal class NetworkingWork
    {
        public void CheckNetworkStatus(object data)
        {
            PrintThreadDetails();

            for (int i = 0; i < 12; i++)
            {
                bool isNetworkUp = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
                Console.WriteLine($"Thread priority {(string)data}; Is network available? Answer: {isNetworkUp}");
                i++;
            }
        }

        internal static void PrintThreadDetails()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine($"The current thread id is {Thread.CurrentThread.ManagedThreadId}.");

            var backgroundString = Thread.CurrentThread.IsBackground ? "a background thread" : "NOT a background thread";

            Console.WriteLine($"This thread is {backgroundString}");

            var threadPoolInfoString = Thread.CurrentThread.IsThreadPoolThread ? "a thread pool thread" : "NOT a thread pool thread";

            Console.WriteLine($"This thread is {threadPoolInfoString}");

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
