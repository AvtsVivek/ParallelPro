

PLINQ works with the same data sources that LINQ to Objects does, namely IEnumerable<> and
IEnumerable. 

PLINQ doesn’t work as a parallel equivalent with the other kinds of LINQ, such as LINQ to
XML and LINQ to SQL. 

However, since the end result of most LINQ queries is an IEnumerable<>, then
PLINQ can be used to parallel process the results from queries on those kinds of data.