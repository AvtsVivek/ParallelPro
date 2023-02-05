using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {

        // create a cancellation token source
        var cancellationTokenSource = new CancellationTokenSource();

        // start a task that will cancel the token source
        // after a few seconds sleep
        Task.Factory.StartNew(() => {
            // sleep for 5 seconds
            Thread.Sleep(5000);
            // cancel the token source
            cancellationTokenSource.Cancel();
            // let the user know
            Console.WriteLine("Token cancelled");
        });

        // create loop options with a token
        var parallelOptions = new ParallelOptions() 
        { 
            CancellationToken = cancellationTokenSource.Token
        };

        try
        {
            // perform a parallel loop specifying the options
            // make this a loop that will take a while to complete
            // so the user has time to cancel
            Parallel.For(0, Int64.MaxValue, parallelOptions, index => {
                // do something just to occupy the cpu for a little
                double result = Math.Pow(index, 3);
                // write out the current index
                Console.WriteLine("Index {0}, result {1}", index, result);
                // put the thread to sleep, just to slow things down
                Thread.Sleep(100);
            });
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Caught cancellation exception...");
        }

        // wait for input before exiting
        // Console.WriteLine("Press enter to finish");
        // Console.ReadLine();
    }
}
