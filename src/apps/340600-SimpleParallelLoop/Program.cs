class SimpleParallelLoop
{
    private static void Main(string[] args)
    {
        // create the arrays to hold the data and the results
        var dataItems = new int[10];
        var resultItems = new double[10];

        // create the data items
        for (int i = 0; i < dataItems.Length; i++)
            dataItems[i] = i;       

        // process the data in a parallel for loop
        Parallel.For(0, dataItems.Length, (index, loopState) => {
            resultItems[index] = Math.Pow(dataItems[index], 2);
        });

        foreach (var item in resultItems)
            Console.WriteLine(item);       

        // wait for input before exiting
        Console.WriteLine("Press enter to finish");
        Console.ReadLine();
    }
}