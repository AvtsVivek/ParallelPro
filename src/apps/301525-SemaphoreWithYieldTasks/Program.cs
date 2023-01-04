using System.Diagnostics;

class Program
{
    // Create the semaphore.
    private static SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(0, 3);
    // A padding interval to make the output more orderly.
    private static int padding;
    static void Main(string[] args)
    {
        Console.WriteLine("{0} tasks can enter the semaphore.", _semaphoreSlim.CurrentCount);

        var taskList = CreateTaskList(5).ToList();

        taskList.ForEach(task => task.Start());

        // Wait for half a second, to allow all the tasks to start and block.
        Thread.Sleep(500);

        // Restore the semaphore count to its maximum value.
        Console.Write("Main thread calls Release(3) --> ");
        _semaphoreSlim.Release(3);
        Console.WriteLine("{0} tasks can enter the semaphore.", _semaphoreSlim.CurrentCount);
        // Main thread waits for the tasks to complete.
        Task.WaitAll(taskList.ToArray());

        Console.WriteLine("Main thread exits.");
    }
    private static IEnumerable<Task> CreateTaskList(int numberOfTasks)
    {
        for (int i = 0; i < numberOfTasks; i++)
        {
            yield return new Task(() =>
            {
                // Each task begins by requesting the semaphore.
                Console.WriteLine("Task {0} begins and waits for the semaphore.", Task.CurrentId);

                var semaphoreCount = 0;
                _semaphoreSlim.Wait();
                try
                {
                    Interlocked.Add(ref padding, 100);

                    Console.WriteLine("Task {0} enters the semaphore.", Task.CurrentId);

                    // The task just sleeps for 1+ seconds.
                    Thread.Sleep(1000 + padding);
                }
                finally
                {
                    semaphoreCount = _semaphoreSlim.Release();
                }
                Console.WriteLine("Task {0} releases the semaphore; previous count: {1}.", Task.CurrentId, semaphoreCount);
            });
        }
    }
}