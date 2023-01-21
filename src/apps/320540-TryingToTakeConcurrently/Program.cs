

// create a blocking collection
using System.Collections.Concurrent;

var blockingCollection = new BlockingCollection<int>();

// create and start a producer
Task.Factory.StartNew(() => {
    // put items into the collectioon
    for (int i = 0; i < 1000; i++)
    {
        blockingCollection.Add(i);
    }
    // mark the collection as complete
    blockingCollection.CompleteAdding();
});

// create and start a producer
Task.Factory.StartNew(() => {
    while (!blockingCollection.IsCompleted)
    {
        // take an item from the collection
        int item = blockingCollection.Take();
        // print out the item
        Console.WriteLine("Item {0}", item);
    }
});

// wait for input before exiting
Console.WriteLine("Press enter to finish");
Console.ReadLine();

