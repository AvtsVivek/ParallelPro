namespace BackgroundPingConsoleApp_cancel
{
    internal class NetworkingWork
    {
        public void CheckNetworkStatus(object data)
        {
            PrintThreadDetails();
            var cancelToken = (CancellationToken)data;
            while (!cancelToken.IsCancellationRequested)
            {
                bool isNetworkUp = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
                Console.WriteLine($"Is network available? Answer: {isNetworkUp}");
            }
        }

        public void CheckNetworkStatus2(object data)
        {
            PrintThreadDetails();
            bool finish = false;
            var cancelToken = (CancellationToken)data;
            cancelToken.Register(() => {
                // Clean up and end pending work
                finish = true;
            });

            while (!finish)
            {
                bool isNetworkUp = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
                Console.WriteLine($"Is network available? Answer: {isNetworkUp}");
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