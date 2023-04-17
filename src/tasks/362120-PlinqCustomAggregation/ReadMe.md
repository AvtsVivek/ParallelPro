

Using Custom Partitioning

PLINQ uses the same partitioning approach that you saw in the previous chapter. You can specify a
customer partitioner for your PLINQ query by using the version of the AsParallel() method that takes
an instance of the System.Collections.Concurrent.Partitioner class as an argument. 

Some earlier showed you how to create a custom dynamic partitioner, where the number of partitions is not
known in advance. Parallel loops support only dynamic partitioners. PLINQ supports the simpler static
partitioners, which are much easier to write, since you can simply break up the data into blocks. 

This shows a simple static partitioner that works on arrays; see the previous chapter for details of the
methods that are required to implement a partitioner.

The StaticPartitioner class in this example breaks up the array of objects into partitions that are
roughly the same size to ensure that each Task that PLINQ assigns to process data receives about the
same amount of data to work on. Earlier example, demonstrates how to use a custom partitioner with PLINQ.

Rather than call AsParallel() on the source data, we call it on an instance of the partitioner, in this case,
the StaticPartitioner class from the Listing.

If you wish to preserve order in PLINQ results with a custom partitioner, your class must derive from
OrderablePartitioner; see the previous chapter for details of this class and how it differs from
Partitioner.