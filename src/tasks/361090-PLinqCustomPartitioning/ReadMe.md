

When you execute a PLINQ query, Tasks are assigned to process blocks of source data to produce a series
of results, which are then consumed—typically by a sequential enumeration. You can control how the
results are passed from the Tasks to the consumer by using the WithMergeOptions() method on an instance of ParallelQuery. 

This method takes a value from the ParallelMergeOptions enumeration. Default, AutoBuggered, NotBuffered and FullyBuffered.

Specifying a merge option can be useful when you have a PLINQ query that takes a significant
amount of time to generate each result item. With the default or fully buffered options, the consumer
will not receive any results for an extended period of time. Listing 6-16 shows how to set the merge
option.