﻿using System.Collections.Concurrent;

namespace BlockingCollectionDemo;
class ConsumingEnumerableDemo
{
    // Demonstrates:
    //      BlockingCollection<T>.Add()
    //      BlockingCollection<T>.CompleteAdding()
    //      BlockingCollection<T>.GetConsumingEnumerable()
    public static async Task BC_GetConsumingEnumerable()
    {
        using (BlockingCollection<int> bc = new BlockingCollection<int>())
        {
            // Kick off a producer task
            var producerTask = Task.Run(async () =>
            {
                for (int i = 0; i < 10; i++)
                {
                    bc.Add(i);
                    Console.WriteLine($"Producing: {i}");

                    await Task.Delay(100); // sleep 100 ms between adds
                }

                // Need to do this to keep foreach below from hanging
                bc.CompleteAdding();
            });

            // Now consume the blocking collection with foreach.
            // Use bc.GetConsumingEnumerable() instead of just bc because the
            // former will block waiting for completion and the latter will
            // simply take a snapshot of the current state of the underlying collection.
            foreach (var item in bc.GetConsumingEnumerable())
            {
                Console.WriteLine($"Consuming: {item}");
            }
            await producerTask; // Allow task to complete cleanup
        }
    }
}