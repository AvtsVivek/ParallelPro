
var manualResetEvent = new ManualResetEventSlim();

// create the cancellation token source
var cancellationTokenSource = new CancellationTokenSource();

var cancellationToken = cancellationTokenSource.Token;

// create and start the task that will wait on the event
var waitingTask = Task.Factory.StartNew(() => {

    while (true)
    {
        Thread.Sleep(1000);
        Console.WriteLine("Waiting one for the task.");
        manualResetEvent.Wait(cancellationToken);
    }
}, cancellationToken);

Console.WriteLine("Press any key to cancel the manual reset event");
Console.ReadKey();

manualResetEvent.Set();


Console.WriteLine("Press any key to reset the manual reset event");
Console.ReadKey();
manualResetEvent.Reset();

Console.WriteLine("Press any key to cancel the manual reset event");
Console.ReadKey();

cancellationTokenSource.Cancel();

Console.WriteLine("Press any key to exit the program");
Console.ReadLine();
Console.WriteLine("Exiting.....");

Console.WriteLine("Bye ...");
