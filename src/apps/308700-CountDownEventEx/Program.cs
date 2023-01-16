
// create a CountDownEvent with a condition
// counter of 5
var countdownEvent = new CountdownEvent(5);

// create a Random that we will use to generate
// sleep intervals
var random = new Random();

// create 5 tasks, each of which will wait for
// a random period and then signal the event
var taskList = new List<Task>();
for (int i = 0; i < 5; i++)
{
    // create the new task
    var task = new Task(() => {
        // put the task to sleep for a random period
        // up to one second
        Thread.Sleep(random.Next(500, 1000));
        // signal the event
        Console.WriteLine("Task {0} signalling event", Task.CurrentId);
        countdownEvent.Signal();
    });

    taskList.Add(task);
};

// create the final task, which will rendezous with the other 5
// using the count down event
taskList.Add(new Task(() => {
    // wait on the event
    Console.WriteLine("Rendezvous task waiting");
    countdownEvent.Wait();
    Console.WriteLine("Event has been set");

}));

// Start the tasks
foreach (var task in taskList)
    task.Start();


Task.WaitAll(taskList.ToArray());

// wait for input before exiting
Console.WriteLine("Press enter to finish");
Console.ReadLine();