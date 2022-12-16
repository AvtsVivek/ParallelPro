using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ConcurrencyPlayground
{
    public class DemoRunner
    {
        private Stopwatch stopwatch = default!;
        private Worker syncService = default!;

        public void Run()
        {
            syncService = new Worker();
            stopwatch = new Stopwatch();
            stopwatch.Start();

            ExecuteSync();

            ExecuteAsyncNoAwait();

            ExecuteAsync().Wait();

            stopwatch.Stop();
        }

        private void ExecuteSync()
        {
            Console.WriteLine("Beginning processing of synchronous code");
            var startTime = stopwatch.ElapsedMilliseconds;

            syncService.DoWork(30000000);
            syncService.DoWork(60000000);
            syncService.DoWork(90000000);

            Console.WriteLine("Finished! Duration: " + (stopwatch.ElapsedMilliseconds - startTime) + " (ms)\n");
        }

        private async Task ExecuteAsync()
        {
            Console.WriteLine("Beginning processing of async code");
            var startTime = stopwatch.ElapsedMilliseconds;

            var task1 = Task.Run(() => syncService.DoWork(30000000));
            var task2 = Task.Run(() => syncService.DoWork(60000000));
            var task3 = Task.Run(() => syncService.DoWork(90000000));

            await Task.WhenAll(task1, task2, task3);

            Console.WriteLine("Finished! Duration: " + (stopwatch.ElapsedMilliseconds - startTime) + " (ms)\n");
        }

        private void ExecuteAsyncNoAwait()
        {
            Console.WriteLine("Beginning processing of async code with no await");
            var startTime = stopwatch.ElapsedMilliseconds;

            // Code initiates the work but does not wait for it to finish
            var task1 = Task.Run(() => syncService.DoWork(30000000));
            var task2 = Task.Run(() => syncService.DoWork(60000000));
            var task3 = Task.Run(() => syncService.DoWork(90000000));

            Console.WriteLine("Finished! Duration: " + (stopwatch.ElapsedMilliseconds - startTime) + " (ms)\n");
        }
    }
}
