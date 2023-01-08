You can register a delegate with a CancellationToken, which will be invoked when the
CancellationTokenSource.Cancel() method is called.

- When you call the WaitOne() method it blocks until the Cancel() method is called on the CancellationTokenSource that was used to create the token whose wait handle you are using.
- This example demos the use of the wait handle for cancellation monitoring. Two Tasks are created, one of which (task2) calls the WaitOne() method, which blocks until the first Task is cancelled.
- 

