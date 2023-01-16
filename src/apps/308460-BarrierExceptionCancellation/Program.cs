using System.Threading;


class Program
{
    static void Main(string[] args)
    {
        // create a barrier
        var barrier = new Barrier(2);

        // create a cancellation token source
        var cancellationTokenSource = new CancellationTokenSource();

        var cancellationToken = cancellationTokenSource.Token;

        // create a task that will complete
        Task.Factory.StartNew(() => {
            Console.WriteLine("Good task starting phase 0");
            barrier.SignalAndWait(cancellationToken);
            Console.WriteLine("Good task starting phase 1");
            barrier.SignalAndWait(cancellationToken);
        }, cancellationToken);

        // create a task that will throw an exception
        // with a selective continuation that will reduce the 
        // particpant count in the barrier
        Task.Factory.StartNew(() => {
            Console.WriteLine("Bad task 1 throwing exception");
            throw new Exception();
        }, cancellationToken).ContinueWith(antecedent => {
            // reduce the particpant count
            Console.WriteLine("Cancelling the token");
            cancellationTokenSource.Cancel();
        }, TaskContinuationOptions.OnlyOnFaulted);

        // wait for input before exiting
        Console.WriteLine("Press enter to finish");
        Console.ReadLine();
    }
}