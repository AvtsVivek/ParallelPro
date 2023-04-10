
PLINQ supports cancellation using the CancellationTokens you have seen throughout this book. You
associate a token with a query using the WithCancellation() extension method. This
demonstrates cancellation by using a token that is cancelled by a Task while the results of a query are
being enumerated.

If a query is cancelled, a System.OperationCanceledException is thrown, and we catch this in
the code that has caused the query to be executed. It will usually be the statements in which we
enumerate the results or in the ForAll() method. PLINQ may continue to process some items after a
cancellation has been performed, so you must not assume that cancellation will terminate your query
immediately.

