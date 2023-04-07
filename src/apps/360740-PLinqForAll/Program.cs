
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Dynamic;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Program
{
    static void Main(string[] args)
    {

        // create some source data
        int[] sourceData = new int[50];

        for (int i = 0; i < sourceData.Length; i++)
        {
            sourceData[i] = i;
        }

        // filter the data and call ForAll()
        sourceData.AsParallel()
            .Where(item => item % 2 == 0)
            .ForAll(item => Console.WriteLine("Item {0} Result {1}", item, Math.Pow(item, 2)));

        // wait for input before exiting
        Console.WriteLine("Press enter to finish");
        Console.ReadLine();
    }
}
