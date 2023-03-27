

PLINQ works with the same data sources that LINQ to Objects does, namely IEnumerable<> and
IEnumerable. 

PLINQ doesn’t work as a parallel equivalent with the other kinds of LINQ, such as LINQ to
XML and LINQ to SQL. 

However, since the end result of most LINQ queries is an IEnumerable<>, then
PLINQ can be used to parallel process the results from queries on those kinds of data.

This shows a PLINQ query that selects even numbers from an array of integers. 

Aside from the call to AsParallel() on the data source, this could be a normal LINQ query.

PLINQ also has two public classes: ParallelEnumerable and ParallelQuery. The ParallelEnumerable
class contains extension methods that operate on the ParallelQuery type. 

ParallelEnumerable contains methods to match each of those in Enumerable, plus some others that allow you to create instances of
ParallelQuery from IEnumerables.

Because the methods in ParallelEnumerable are parallel implementations of those in Enumerable,
once you have created a ParallelQuery, the magic of extension methods means that your LINQ query
will use methods from ParallelEnumerable, rather than Enumerable, and you will be using PLINQ.
This neat integration extends to the C# keywords. 

If you apply them to an instance of ParallelQuery, the query will be executed using PLINQ. 