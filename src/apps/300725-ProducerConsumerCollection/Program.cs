using System.Collections.Concurrent;

namespace ProducerConsumerCollection;

public class Program
{
    static void Main()
    {
        TestSafeStack();

        // Keep the console window open in debug mode.
        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }

    // Test our implementation of IProducerConsumerCollection(T)
    // Demonstrates:
    //      IPCC(T).TryAdd()
    //      IPCC(T).TryTake()
    //      IPCC(T).CopyTo()
    static void TestSafeStack()
    {
        SafeStack<int> stack = new SafeStack<int>();
        IProducerConsumerCollection<int> ipcc = (IProducerConsumerCollection<int>)stack;

        // Test Push()/TryAdd()
        stack.Push(10); Console.WriteLine("Pushed 10");
        ipcc.TryAdd(20); Console.WriteLine("IPCC.TryAdded 20");
        stack.Push(15); Console.WriteLine("Pushed 15");

        int[] testArray = new int[3];

        // Try CopyTo() within boundaries
        try
        {
            ipcc.CopyTo(testArray, 0);
            Console.WriteLine("CopyTo() within boundaries worked, as expected");
        }
        catch (Exception e)
        {
            Console.WriteLine("CopyTo() within boundaries unexpectedly threw an exception: {0}", e.Message);
        }

        // Try CopyTo() that overflows
        try
        {
            ipcc.CopyTo(testArray, 1);
            Console.WriteLine("CopyTo() with index overflow worked, and it SHOULD NOT HAVE");
        }
        catch (Exception e)
        {
            Console.WriteLine("CopyTo() with index overflow threw an exception, as expected: {0}", e.Message);
        }

        // Test enumeration
        Console.Write("Enumeration (should be three items): ");
        foreach (int item in stack) Console.Write("{0} ", item);
        Console.WriteLine("");

        // Test TryPop()
        int popped = 0;
        if (stack.TryPop(out popped))
        {
            Console.WriteLine("Successfully popped {0}", popped);
        }
        else
        {
            Console.WriteLine("FAILED to pop!!");
        }

        // Test Count
        Console.WriteLine("stack count is {0}, should be 2", stack.Count);

        // Test TryTake()
        if (ipcc.TryTake(out popped))
        {
            Console.WriteLine("Successfully IPCC-TryTaked {0}", popped);
        }
        else
        {
            Console.WriteLine("FAILED to IPCC.TryTake!!");
        }
    }
}