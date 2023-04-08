

PLINQ queries are not executed until the results are required, which is known as deferred or lazy
execution. This is the same model as for LINQ, and the idea is that you don’t incur the cost of performing
a query if you don’t use the results. This ex demonstrates how this works.
