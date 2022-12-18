

https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.orderablepartitioner-1

- Not very clear about this. Need to look into this once again.

- When you use a parallel loop, your data is broken down into a series of blocks (known as chunks or
partitions), which are processed concurrently. The detail of how your data is partitioned depends on the
partitioning strategy applied to your loop

- Take a look at 5th chapter in the book Pro_.NET_4_Parallel_Programming_in_CSharp

- 