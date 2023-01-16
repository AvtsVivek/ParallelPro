
var manualResetEvent = new ManualResetEventSlim();

// create the cancellation token source
var cancellationTokenSource = new CancellationTokenSource();

var cancellationToken = cancellationTokenSource.Token;

// create and start the task that will wait on the event
var waitingTask = Task.Factory.StartNew(() => {
    while (true)
    {
        // wait on the primitive
        manualResetEvent.Wait(cancellationToken);
        // print out a message
        Console.WriteLine("Waiting task active");
    }
}, cancellationToken);

// create and start the signalling task
var signallingTask = Task.Factory.StartNew(() => {
    // create a random generator for sleep periods
    var random = new Random();
    // loop while the task has not been cancelled
    while (!cancellationToken.IsCancellationRequested)
    {
        // go to sleep for a random period
        cancellationToken.WaitHandle.WaitOne(random.Next(500, 2000));
        // set the event
        manualResetEvent.Set();
        Console.WriteLine("Event set");
        // go to sleep again
        cancellationToken.WaitHandle.WaitOne(random.Next(500, 2000));
        // reset the event
        manualResetEvent.Reset();
        Console.WriteLine("Event reset");
    }
    // if we reach this point, we know the task has been cancelled
    cancellationToken.ThrowIfCancellationRequested();
}, cancellationToken);

// ask the user to press return before we cancel 
// the token and bring the tasks to an end
Console.WriteLine("Press enter to cancel tasks");
Console.ReadLine();

// cancel the token source and wait for the tasks
cancellationTokenSource.Cancel();

try
{
    Task.WaitAll(waitingTask, signallingTask);
}
catch (AggregateException)
{
    // discard exceptions
}

// wait for input before exiting
Console.WriteLine("Press enter to finish");
Console.ReadLine();