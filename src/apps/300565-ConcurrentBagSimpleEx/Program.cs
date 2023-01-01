using System.Collections.Concurrent;

// create a shared collection 
ConcurrentBag<int> sharedConcurrentBag = new ();

// Populate the bag
for (int i = 0; i < 1000; i++)
    sharedConcurrentBag.Add(i);

// define a counter for the number of processed items
var itemCount = 0;

// create an array of tasks
var taskList = new List<Task>();

for (var i = 0; i < 10; i++)
{
    // create the new task
    var task = new Task(() => {
        while (sharedConcurrentBag.Count > 0)
        {
            // define a variable for the dequeue requests
            int queueElement;
            // take an item from the bag
            bool gotElement = sharedConcurrentBag.TryTake(out queueElement);
            // increment the count of items processed
            if (gotElement)
                Interlocked.Increment(ref itemCount);

        }
    });

    taskList.Add(task);
}

// Start all of the tasks
taskList.ForEach(task => task.Start());

// wait for the tasks to complete
Task.WaitAll(taskList.ToArray());


// report on the number of items processed
Console.WriteLine("Items processed: {0}", itemCount);

// wait for input before exiting
Console.WriteLine("Press enter to finish");
Console.ReadLine();

Console.WriteLine("Done. ..");