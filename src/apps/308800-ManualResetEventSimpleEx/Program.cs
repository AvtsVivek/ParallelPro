
var manualResetEvent = new ManualResetEventSlim();

// create and start the task that will wait on the event
var waitingTask = Task.Factory.StartNew(() => {
    // wait on the primitive
    Console.WriteLine("Waiting for the task.");
    manualResetEvent.Wait();
    Console.WriteLine("Done!!!!");
});

Console.WriteLine("Press any key to reset the manual reset event");
Console.ReadKey();
manualResetEvent.Set();

Console.WriteLine("Exiting.....");

Console.WriteLine("Bye ...");
