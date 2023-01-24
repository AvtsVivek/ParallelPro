

// create a cancellation token source
var cancellationTokenSource = new CancellationTokenSource();

var cancellationToken = cancellationTokenSource.Token;

var antecedentTasks = new Task<int>[2];

antecedentTasks[0] = new Task<int>(() => {
    while (true)
    {
        // sleep 
        Thread.Sleep(100000);
    }
});

antecedentTasks[1] = new Task<int>(() => {
    // wait for the token to be cancelled
    cancellationToken.WaitHandle.WaitOne();
    // throw a cancellation exceptioon
    cancellationToken.ThrowIfCancellationRequested();
    // return a result to satisfy the compiler
    return 200;
}, cancellationToken);

Task.Factory.ContinueWhenAny(antecedentTasks, (Task<int> antecedent) => {
    Console.WriteLine("Result of antecedent is {0}", antecedent.Result);
});

// start the tasks
antecedentTasks[1].Start();
antecedentTasks[0].Start();

// prompt the user and cancel the token
Console.WriteLine("Press enter to cancel token");
Console.ReadLine();
cancellationTokenSource.Cancel();

// wait for input before exiting
Console.WriteLine("Press enter to finish");
Console.ReadLine();
