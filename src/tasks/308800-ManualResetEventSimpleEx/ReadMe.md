
- Barrier Class. Enables multiple tasks to cooperatively work on an algorithm in parallel through multiple phases.

- The second technique is to use a CancellationToken when creating the Tasks and use a version of the Barrier.SignalAndWait() method that takes a CancellationToken as an argument. 

- A selective continuation Task cancels the token, which causes calls to SignalAndWait() to throw an OperationCancelledException, stopping all of the Tasks from continuing. This technique works if you don’t want any of the Tasks to continue if any of them throw an exception. 

- The example demonstrates this technique. Remember that the exception that was thrown in the first place is unhandled and will have to be dealt with.

