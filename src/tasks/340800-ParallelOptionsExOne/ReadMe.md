# Parallel Options. 

- MaxDegreeOfParallelism: Get or set the maximum concurrency for a parallel loop. Setting this property to -1 means no concurrency limit is set.

- Setting Parallel Loop Options You can influence the behavior of parallel loops by supplying an instance of the ParallelOptions class as an argument to Parallel.For() or Parallel.ForEach()

- The property, maxDegreeOfParallelism, allows you to limit the number of Tasks that are executed concurrently to perform a parallel loop. Setting a high limit does not increase the degree of parallelism; all you can do with this property is set a limit for the concurrency. Specifying a value of 0 (i.e., no concurrency at all) will cause Parallel.For() and Parallel.ForEach() to throw an exception.

- So maxDegreeOfParallelism = 1 means only one task or thread. You can see the result. Everyting is sequencial.
- 