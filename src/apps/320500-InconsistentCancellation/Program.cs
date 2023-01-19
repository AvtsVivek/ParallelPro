

// create a token source
var cancellationTokenSource = new CancellationTokenSource();
var cancellationToken = cancellationTokenSource.Token;

// create the antecedent task
Task<int> task1 = new Task<int>(() => {
    // wait for the token to be cancelled
    cancellationToken.WaitHandle.WaitOne();
    // throw the cancellation exception
    cancellationToken.ThrowIfCancellationRequested();
    // return the result - this code will
    // never be reached but is required to 
    // satisfy the compiler
    return 100;
}, cancellationToken);

// create a continuation
// *** BAD CODE ***
Task task2 = task1.ContinueWith((Task<int> antecedent) => {
    // read the antecedent result without checking
    // the status of the task
    Console.WriteLine("Antecedent result: {0}", antecedent.Result);
});

// create a continuation, but use a token
Task task3 = task1.ContinueWith((Task<int> antecedent) => {
    // this task will never be executed 
}, cancellationToken);

// create a continuation that checks the status
// of the antecedent
Task task4 = task1.ContinueWith((Task<int> antecedent) => {
    if (antecedent.Status == TaskStatus.Canceled)
    {
        Console.WriteLine("Antecedent cancelled");
    }
    else
    {
        Console.WriteLine("Antecedent Result: {0}", antecedent.Result);
    }
});

// prompt the user and cancel the token
Console.WriteLine("Press enter to cancel token");
Console.ReadLine();
cancellationTokenSource.Cancel();

// wait for input before exiting
Console.WriteLine("Press enter to finish");
Console.ReadLine();