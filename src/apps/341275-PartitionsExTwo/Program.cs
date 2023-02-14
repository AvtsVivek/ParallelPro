using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Dynamic;

class Program
{
    static void Main(string[] args)
    {

        if (args.Length != 2)
        {
            Console.WriteLine("Only 2 command line args is expected. Returning....");
            return;
        }

        if (!int.TryParse(args[0], out var rangeSize))
        {
            Console.WriteLine("The first arg must be integer. Exiting ...... ");
            return;
        }

        if (!int.TryParse(args[1], out var partitionCount))
        {
            Console.WriteLine("The second arg must be integer. Exiting ...... ");
            return;
        }

        // create the results array
        var resultData = new double[100];

        // created a partioner that will chunk the data
        OrderablePartitioner<Tuple<int, int>> chunkPart = Partitioner.Create(0, resultData.Length, rangeSize);

        IList<IEnumerator<Tuple<int, int>>> listEnumeratorTupleIntInt = chunkPart.GetPartitions(partitionCount);

        foreach (var enumeratorTupleIntInt in listEnumeratorTupleIntInt)
        {
            while (enumeratorTupleIntInt.MoveNext())
            {
                Tuple<int, int> item = enumeratorTupleIntInt.Current;
                
                Console.WriteLine($"TupleItem {item.Item1} and {item.Item2}");
            }
        }

        IList<IEnumerator<KeyValuePair<long, Tuple<int, int>>>> listEnumeratorTupleIntIntOrdrable = chunkPart.GetOrderablePartitions(partitionCount);

        foreach (var enumeratorTupleIntInt in listEnumeratorTupleIntIntOrdrable)
        {
            while (enumeratorTupleIntInt.MoveNext())
            {
                KeyValuePair<long, Tuple<int, int>> tupleKeyValuePair = enumeratorTupleIntInt.Current;

                Console.WriteLine($"TupleItem {tupleKeyValuePair.Key} and item1 {tupleKeyValuePair.Value.Item1} item2 {tupleKeyValuePair.Value.Item2}");
            }
        }
    }
}
