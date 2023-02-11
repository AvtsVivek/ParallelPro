- Pallel with TLS Thread Local Storage
- In this example, the first and second arguments to Parallel.For() are the start and end index values. 
- The third argument is a delegate that initializes an instance of the thread-local type. In this example, this is an int with a value of 0.
```cs
() => 0
```

- When you use a parallel loop, your data is broken down into a series of blocks (known as chunks or partitions), which are processed concurrently. The detail of how your data is partitioned depends on the
partitioning strategy applied to your loop, 

- The TLS initialization delegate is called once for each data partition. In our example, it creates an int with a value of 0. You don’t need to worry about the details of partitioning when using TLS, other than to know that the delegate may be called several times.

- The fourth argument is the loop body, an Action which has three arguments: the current iteration index, the ParallelLoopState for the loop, and the current value of the thread-local variable. This Action is called once per index value and must return a result that is the same type as the thread-local variable.
In the example, we add the current index to the thread-local variable and return the result.

- The final argument is an Action that is passed the TLS value and is called once per data partition.
This action is the complement of the TLS initialization delegate. In the example, we use the Interlocked
class to safely update the overall total.
- The benefit of using TLS in a loop is that you don’t need to synchronize access to the variable in
your loop body. 
- The Interlocked class is only used once per data partition. 
- The default practitioner usually breaks up the data in example into four partitions, resulting
in 96 fewer calls to Interlocked than the original loop. 
- The exact savings depends on the source data and the partition strategy used; the greater the number of iterations, the greater the savings in overhead.
- This illustrates a Parallel.ForEach() loop with TLS and shows that you can use any of the synchronization techniques we used earlier to coordinate the TLS value with variables outside of the parallel loop.

- Not clear what data partition is. Need to look into.