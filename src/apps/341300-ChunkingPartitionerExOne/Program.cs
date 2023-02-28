using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Dynamic;

namespace ChunkingPartitionerExOne
{
    class Program
    {
        static void Main(string[] args)
        {
            // create a random number source
            var random = new Random();

            // create the source data
            var sourceData = new WorkItem[10000];

            for (int i = 0; i < sourceData.Length; i++)
            {
                sourceData[i] = new WorkItem() { WorkDuration = random.Next(1, 11) };
            }

            // created the contentual partitioner
            var contextPartitioner = new ContextPartitioner(sourceData, 100);

            // create the parallel 
            Parallel.ForEach(contextPartitioner, item => {
                // perform the work item
                item.PerformWork();
            });

            // wait for input before exiting
            Console.WriteLine("Press enter to finish");
            Console.ReadLine();
        }
    }
}