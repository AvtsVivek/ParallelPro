using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {


        var total = 0;
        Parallel.For(0, 100, () => 0, (int index, ParallelLoopState loopState, int tlsValue) => {
            tlsValue += index; 
            return tlsValue;
        }, value => Interlocked.Add(ref total, value));

        Console.WriteLine($"The total is {total} and the expected is {99*100/2}"); // n(n+1)/2

        // wait for input before exiting
        // Console.WriteLine("Press enter to finish");
        // Console.ReadLine();
    }
}
