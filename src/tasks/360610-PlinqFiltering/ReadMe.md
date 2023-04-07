


You may have noticed that there is little difference between a PLINQ projection and a parallel loop—
each and every value in a data source is projected to a corresponding result.
This is true, but the power of PLINQ is that it builds on the rich feature set of LINQ to allow you to
do things that would be extremely cumbersome in parallel loops. A common example is filtering, where
a subset of items from the source data are processed. This example demonstrates a simple PLINQ filtering
query, which projects results for even numbers only.

