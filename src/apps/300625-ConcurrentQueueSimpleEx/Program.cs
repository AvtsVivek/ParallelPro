using System.Collections.Concurrent;

// Construct a ConcurrentQueue.
ConcurrentQueue<int> concurrentQueue = new();

// Populate the queue.
for (int i = 0; i < 1000; i++)
    concurrentQueue.Enqueue(i);

// define a counter for the number of processed items
var itemCount = 0;

// create an array of tasks
var taskList = new List<Task>();

// create tasks to process the list
// var tasks = new Task[10];

for (var i = 0; i < 10; i++)
{
    // create the new task
    var task = new Task(() => {
        while (concurrentQueue.Count > 0)
        {
            // define a variable for the dequeue requests
            int queueElement;
            // take an item from the queue
            bool elementSuccessifullyDequed = concurrentQueue.TryDequeue(out queueElement);
            // increment the count of items processed
            if (elementSuccessifullyDequed)
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