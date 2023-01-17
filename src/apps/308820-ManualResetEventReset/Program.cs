
var manualResetEvent = new ManualResetEventSlim();

// create and start the task that will wait on the event
var waitingTask = Task.Factory.StartNew(() => {

    while (true)
    {
        // wait on the primitive
        Task.Delay(300).Wait();
        Console.WriteLine("Waiting one for the task.");
        manualResetEvent.Wait();
        Task.Delay(300).Wait();
        Console.WriteLine("Waiting two for the task.");
        manualResetEvent.Wait();
        Task.Delay(300).Wait();
        Console.WriteLine("Waiting three for the task.");
        manualResetEvent.Wait();
    }
});

Console.WriteLine("Press any key to set the manual reset event");
Console.ReadKey();
manualResetEvent.Set();

Console.WriteLine("Press any key to reset the manual reset event");
Console.ReadKey();
manualResetEvent.Reset();

Console.WriteLine("Press any key to exit the program");
Console.ReadLine();
Console.WriteLine("Exiting.....");

Console.WriteLine("Bye ...");
