using System.Collections.Concurrent;
class SimpleObject
{
    public int Counter
    {
        get;
        set;
    }
}

class ReusingObjectsInProducers
{
    static void Main(string[] args)
    {
        // create a blocking collection
        var blockingCollection = new BlockingCollection<SimpleObject>();

        // create and start a consumer
        var consumer = Task.Factory.StartNew(() => {
            // define a data item to use in the loop
            SimpleObject item;
            while (!blockingCollection.IsCompleted)
            {
                if (blockingCollection.TryTake(out item!))
                {
                    Console.WriteLine("Item counter {0}", item.Counter);
                }
            }
        });

        // create and start a producer
        Task.Factory.StartNew(() => {
            // create a data item to use in the loop
            SimpleObject item = new SimpleObject();
            for (int i = 0; i < 100; i++)
            {
                // set the numeric value
                item.Counter = i;
                // add the item to the collection
                blockingCollection.Add(item);
            }
            // mark the collection as finished
            blockingCollection.CompleteAdding();
        });

        // wait for the consumer to finish
        consumer.Wait();

        // wait for input before exiting
        Console.WriteLine("Press enter to finish");
        Console.ReadLine();
    }
}
