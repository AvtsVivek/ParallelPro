using System.Diagnostics;

namespace OrderablePartitionerDemo
{
    class Program
    {
        private static HttpClient _httpClient = null!;
        private static SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);
        private static bool _useSemaphore = false;
        private static string _webUrl = string.Empty;
        private static int _callCount = 0;
        static void Main(string[] args)
        {
            if(!ReadCommandLineArgsAndGetReady(args))
                return;

            var taskArray = CreateCalls().ToArray();

            try
            {
                // wait for the tasks to complete
                Task.WaitAll(taskArray);
            }
            catch (AggregateException exception)
            {
                Console.WriteLine("Exception happened ...");
                Console.WriteLine(exception.Message);
                return;
            }
        }

        private static IEnumerable<Task> CreateCalls()
        {
            for (int i = 0; i < _callCount; i++)
                yield return CallWebUrl(_useSemaphore);

        }

        private static async Task CallWebUrl(bool useSemaphore)
        {

            if (useSemaphore)
            {
                await _semaphoreSlim.WaitAsync();
                var response = await _httpClient.GetAsync(_webUrl);
                _semaphoreSlim.Release();
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var response = await _httpClient.GetAsync(_webUrl);
                Console.WriteLine(response.StatusCode);
            }
        }

        private static bool ReadCommandLineArgsAndGetReady(string[] args)
        {
            var callCountString = string.Empty;
            var httpClientTimeoutString = string.Empty;
            var useSemaphoreString = string.Empty;
            var webUrlString = string.Empty;

            if (args.Length == 4)
            {
                callCountString = args[0];
                httpClientTimeoutString = args[1];
                useSemaphoreString = args[2];
                webUrlString = args[3];
            }
            else
            {
                Console.WriteLine("In suffecients params");
                return false;
            }

            if (!int.TryParse(callCountString, out _callCount))
            {
                Console.WriteLine("call count string is invalid. It should be an integer");
                return false;
            }

            int httpClientTimeoutInSeconds = 0;

            if (!int.TryParse(httpClientTimeoutString, out httpClientTimeoutInSeconds))
            {
                Console.WriteLine("HttpClient Timeout string is invalid. It should be an integer");
                return false;
            }

            _httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(httpClientTimeoutInSeconds) };
            if (!bool.TryParse(useSemaphoreString, out _useSemaphore))
            {
                Console.WriteLine("UseSemaphore string is invalid. It should be 'true' or 'false'");
                return false;
            }

            _webUrl= webUrlString;

            return true;
        }
    }
}