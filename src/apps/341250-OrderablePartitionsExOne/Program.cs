using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Dynamic;

class Program
{
    static void Main(string[] args)
    {

        if (args.Length != 1)
        {
            Console.WriteLine("Only 1 command line arg is expected. Returning....");
            return;
        }



        if (!int.TryParse(args[0], out var rangeSize))
        {
            Console.WriteLine("The arg must be integer. Exiting ...... ");
            return;
        }

        // create the results array
        var resultData = new double[100];

        // created a partioner that will chunk the data
        OrderablePartitioner<Tuple<int, int>> chunkPart = Partitioner.Create(0, resultData.Length, rangeSize);

        IEnumerable<Tuple<int, int>> dynamicPartitions = chunkPart.GetDynamicPartitions();

        dynamicPartitions.ToList().ForEach(tupleObject => { 
            Console.WriteLine($"Itme1: {tupleObject.Item1} Item2: {tupleObject.Item2} ");
        });

        IEnumerable<KeyValuePair<long, Tuple<int, int>>> orderableDynamicPartitions = chunkPart.GetOrderableDynamicPartitions();

        orderableDynamicPartitions.ToList().ForEach(tupleObjectKeyValuePair => {
            Console.WriteLine($"Key: {tupleObjectKeyValuePair.Key} value.Item1: {tupleObjectKeyValuePair.Value.Item1} value.Item2: {tupleObjectKeyValuePair.Value.Item2} ");
        });

        IList<IEnumerator<Tuple<int, int>>> listEnumeratorTupleIntInt = chunkPart.GetPartitions(1);

        foreach (var enumeratorTupleIntInt in listEnumeratorTupleIntInt)
        {
            while (enumeratorTupleIntInt.MoveNext())
            {
                Tuple<int, int> item = enumeratorTupleIntInt.Current;
                
                Console.WriteLine($"Here we go {item.Item1} and {item.Item2}");
            }
        }

        //// perform the loop in chunks
        //Parallel.ForEach(chunkPart, chunkRange => {
        //    // iterate through all of the values in the chunk range
        //    for (int i = chunkRange.Item1; i < chunkRange.Item2; i++)
        //    {
        //        // resultData[i] = Math.Pow(i, 2);

        //        Console.WriteLine($"Here we go...{i}");
        //    }
        //});

        // wait for input before exiting
        // Console.WriteLine("Press enter to finish");
        // Console.ReadLine();

    }
}
