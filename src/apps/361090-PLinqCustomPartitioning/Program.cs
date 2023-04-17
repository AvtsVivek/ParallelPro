class Program
{
    static void Main(string[] args)
    {
        // create some source data
        var sourceData = new int[10];

        for (int i = 0; i < sourceData.Length; i++)
            sourceData[i] = i;
        

        // create the partitioner
        var partitioner = new StaticPartitioner<int>(sourceData);

        // define a query
        IEnumerable<double> results =
            partitioner.AsParallel()
            .Select(item => Math.Pow(item, 2));

        // enumerate the query results
        foreach (double d in results)
            Console.WriteLine("Enumeration got result {0}", d);

        // wait for input before exiting
        Console.WriteLine("Press enter to finish");
        Console.ReadLine();

    }
}
