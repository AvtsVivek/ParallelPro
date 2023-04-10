
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Dynamic;

class Program
{
    static void Main(string[] args)
    {
        // create a cancellation token source
        var cancellationtokenSource
            = new CancellationTokenSource();

        // create some source data
        int[] sourceData = new int[1000000];
        for (int i = 0; i < sourceData.Length; i++)
        {
            sourceData[i] = i;
        }

        // define a query that supports cancellation
        IEnumerable<double> results = sourceData
            .AsParallel()
            .WithCancellation(cancellationtokenSource.Token)
            .Select(item => {
                // return the result value
                return Math.Pow(item, 2);
            });

        // create a task that will wait for 5 seconds
        // and then cancel the token
        Task.Factory.StartNew(() => {
            Thread.Sleep(5000);
            cancellationtokenSource.Cancel();
            Console.WriteLine("Token source cancelled");
        });

        try
        {
            // enumerate the query results
            foreach (double d in results)
            {
                Console.WriteLine("Result: {0}", d);
            }
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Caught cancellation exception");
        }

        // wait for input before exiting
        Console.WriteLine("Press enter to finish");
        Console.ReadLine();
    }
}
