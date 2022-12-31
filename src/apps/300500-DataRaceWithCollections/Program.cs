using System.Threading.Tasks;

class Program
{
    private static void Main(string[] args)
    {
        // create a shared collection 
        var sharedQueue = new Queue<int>();

        // populate the collection with items to process
        for (int i = 0; i < 1000; i++)
            sharedQueue.Enqueue(i);       

        // define a counter for the number of processed items
        var itemCount = 0;

        // create an array of tasks
        var taskList = new List<Task>();

        // create tasks to process the list
        // var tasks = new Task[10];

        for (int i = 0; i < 10; i++)
        {
            // create the new task
            var task = new Task(() => {
                while (sharedQueue.Count > 0)
                {
                    // take an item from the queue
                    int item = sharedQueue.Dequeue();
                    // increment the count of items processed
                    Interlocked.Increment(ref itemCount);
                }
            });

            taskList.Add(task);
        }

        // wait for all of the tasks to complete
        // and wrap the method in a try...catch block
        try
        {
            // Start all of the tasks
            taskList.ForEach(task => task.Start());

            // wait for the tasks to complete
            Task.WaitAll(taskList.ToArray());
        }
        catch (AggregateException ex)
        {
            // enumerate the exceptions that have been aggregated
            foreach (Exception inner in ex.InnerExceptions)
            {
                Console.WriteLine("Exception type {0} from {1}", inner.GetType(), inner.Source);
                Console.WriteLine($"Exception message is {inner.Message} ");
            }
        }

        // report on the number of items processed
        Console.WriteLine("Items processed: {0}", itemCount);

        // wait for input before exiting
        Console.WriteLine("Press enter to finish");
        Console.ReadLine();

        Console.WriteLine("Done. ..");
    }
}
