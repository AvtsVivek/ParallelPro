using System.Collections.Concurrent;

namespace BlockingCollectionDemo;
class AddTakeDemo
{
    // Demonstrates:
    //      BlockingCollection<T>.Add()
    //      BlockingCollection<T>.Take()
    //      BlockingCollection<T>.CompleteAdding()
    public static async Task BC_AddTakeCompleteAdding()
    {
        using (BlockingCollection<int> blockingCollection = new BlockingCollection<int>())
        {
            // Spin up a Task to populate the BlockingCollection
            Task t1 = Task.Run(() =>
            {
                blockingCollection.Add(1);
                blockingCollection.Add(2);
                blockingCollection.Add(3);
                blockingCollection.CompleteAdding();
            });

            // Spin up a Task to consume the BlockingCollection
            Task t2 = Task.Run(() =>
            {
                try
                {
                    // Consume consume the BlockingCollection
                    while (true) Console.WriteLine(blockingCollection.Take());
                }
                catch (InvalidOperationException)
                {
                    // An InvalidOperationException means that Take() was called on a completed collection
                    Console.WriteLine("That's All!");
                }
            });

            await Task.WhenAll(t1, t2);
        }
    }
}
