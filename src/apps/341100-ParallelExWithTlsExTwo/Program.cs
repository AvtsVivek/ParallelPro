using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        // create a running total of matched words
        var matchedWords = 0;
        // create a lock object
        var lockObj = new object();

        // define the source data
        var dataItems = new string[] { "an", "apple", "a", "day",
                    "keeps", "the", "doctor", "away" };

        // perform a parallel foreach loop with TLS
        Parallel.ForEach(dataItems, () => 0, (string item, ParallelLoopState loopState, int tlsValue) => {
                // increment the tls value if the item
                // contains the letter 'a'
                if (item.Contains("a"))
                {
                    tlsValue++;
                }
                return tlsValue;
            },
            tlsValue => {
                lock (lockObj)
                {
                    matchedWords += tlsValue;
                }
            });

        Console.WriteLine("Matches: {0}", matchedWords);

        // wait for input before exiting
        Console.WriteLine("Press enter to finish");
        Console.ReadLine();

    }
}
