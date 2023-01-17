
var autoResetEvent = new AutoResetEvent(true);

// create and start the task that will wait on the event
var waitingTask = Task.Factory.StartNew(() => {
    // wait on the primitive
    Console.WriteLine("Waiting one for the task.");
    autoResetEvent.WaitOne();
    Console.WriteLine("Waiting two for the task.");
    autoResetEvent.WaitOne();
    Console.WriteLine("Waiting three for the task.");
    autoResetEvent.WaitOne();
    Console.WriteLine("Waiting four for the task.");
    autoResetEvent.WaitOne();
    Console.WriteLine("Done!!!!");
});

Console.WriteLine("Press any key to reset the auto reset event");
Console.ReadKey();
autoResetEvent.Set();

Console.WriteLine("Press any key to reset the auto reset event");
Console.ReadKey();
autoResetEvent.Set();

Console.WriteLine("Press any key to reset the auto reset event");
Console.ReadKey();
autoResetEvent.Set();

Console.WriteLine("Exiting.....");

Console.WriteLine("Bye ...");
