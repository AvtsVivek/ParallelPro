class Program
{
    private static void Main(string[] args)
    {
        var synchronizedCache = new SynchronizedCache();
        var taskList = new List<Task>();
        var itemsWritten = 0;

        // Execute a writer.
        taskList.Add(Task.Run(() => {
            String[] vegetables = { "broccoli", "cauliflower", "carrot", "sorrel", "baby turnip", "beet", "brussel sprout", 
                "cabbage", "plantain", "spinach", "grape leaves", "lime leaves", "corn", "radish", "cucumber", "raddichio", "lima beans" };

            for (int counter = 1; counter <= vegetables.Length; counter++)
                synchronizedCache.Add(counter, vegetables[counter - 1]);

            itemsWritten = vegetables.Length;
            
            Console.WriteLine($"Task {Task.CurrentId} wrote {itemsWritten} items {Environment.NewLine}");

        }));

        // Execute two readers, one to read from first to last and the second from last to first.
        for (var ctr = 0; ctr <= 1; ctr++)
        {
            bool desc = ctr == 1;
            taskList.Add(Task.Run(() => {
                int start, last, step;
                int items;
                do
                {
                    String output = String.Empty;
                    items = synchronizedCache.Count;
                    if (!desc)
                    {
                        start = 1;
                        step = 1;
                        last = items;
                    }
                    else
                    {
                        start = items;
                        step = -1;
                        last = 1;
                    }

                    for (int index = start; desc ? index >= last : index <= last; index += step)
                        output += String.Format("[{0}] ", synchronizedCache.Read(index));

                    Console.WriteLine("Task {0} read {1} items: {2}\n",
                                      Task.CurrentId, items, output);
                } while (items < itemsWritten | itemsWritten == 0);
            }));
        }

        // Execute a red/update task.
        taskList.Add(Task.Run(() => {
            Thread.Sleep(100);
            for (int counter = 1; counter <= synchronizedCache.Count; counter++)
            {
                String value = synchronizedCache.Read(counter);
                if (value == "cucumber")
                    if (synchronizedCache.AddOrUpdate(counter, "green bean") != SynchronizedCache.AddOrUpdateStatus.Unchanged)
                        Console.WriteLine("Changed 'cucumber' to 'green bean'");
            }
        }));

        // Wait for all three tasks to complete.
        Task.WaitAll(taskList.ToArray());

        // Display the final contents of the cache.
        Console.WriteLine();
        Console.WriteLine("Values in synchronized cache: ");

        for (int counter = 1; counter <= synchronizedCache.Count; counter++)
            Console.WriteLine("   {0}: {1}", counter, synchronizedCache.Read(counter));

    }
}
