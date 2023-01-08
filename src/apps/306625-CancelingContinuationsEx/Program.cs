class ContinueWhenAnyEx
{
    static void Main(string[] args)
    {

        // create a cancellation token source
        CancellationTokenSource cancellationTokenSource = new ();

        var cancellationToken = cancellationTokenSource.Token;

        // create the antecedent task
        var antecedentTask = new Task(() => {
            // write out a message
            Console.WriteLine("Antecedent running");
            // wait indefinately on the token wait handle
            cancellationToken.WaitHandle.WaitOne();
            // handle the cancellation exception
            cancellationToken.ThrowIfCancellationRequested();
        }, cancellationToken);

        // create a selective continuation 
        var neverScheduledTask = antecedentTask.ContinueWith(antecedent => {
            // write out a message
            Console.WriteLine("This task will never be scheduled");
        }, cancellationToken);

        // create a bad selective contination 
        var badSelectiveTask = antecedentTask.ContinueWith(antecedent => {
            // write out a message
            Console.WriteLine("This task will never be scheduled");
        }, cancellationToken, TaskContinuationOptions.OnlyOnCanceled);

        // create a good selective contiuation
        var continuationTask = antecedentTask.ContinueWith(antecedent => {
            // write out a message
            Console.WriteLine("Continuation running");
        }, TaskContinuationOptions.OnlyOnCanceled);

        // start the task
        antecedentTask.Start();

        // prompt the user so they can cancel the token
        Console.WriteLine("Press enter to cancel token");
        Console.ReadLine();
        // cancel the token source
        cancellationTokenSource.Cancel();

        // wait for the good continuation to complete
        continuationTask.Wait();

        // wait for input before exiting
        Console.WriteLine("Press enter to finish");
        Console.ReadLine();

        Console.WriteLine("Done ... ");
    }
}