
var manualResetEvent = new ManualResetEventSlim();

// create and start the task that will wait on the event
var waitingTask = Task.Factory.StartNew(() => {
    // wait on the primitive
    Console.WriteLine("Waiting one for the task.");
    manualResetEvent.Wait();
    Console.WriteLine("Waiting two for the task.");
    manualResetEvent.Wait();
    Console.WriteLine("Waiting three for the task.");
    manualResetEvent.Wait();
    Console.WriteLine("Waiting four for the task.");
    manualResetEvent.Wait();
    Console.WriteLine("Waiting five for the task.");
    manualResetEvent.Wait();
    Console.WriteLine("Waiting six for the task.");
    manualResetEvent.Wait();
    Console.WriteLine("Done!!!!");
});

Console.WriteLine("Press any key to reset the manual reset event");
Console.ReadKey();
manualResetEvent.Set();


Console.WriteLine("Exiting.....");

Console.WriteLine("Bye ...");
