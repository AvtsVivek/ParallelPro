
- Barrier Class. Enables multiple tasks to cooperatively work on an algorithm in parallel through multiple phases.
- 
- There is a deadlock if a Task doesn’t signal the Barrier, because it throws an exception. 
- The current phase will never end, and the waiting Tasks will never be released.
- Abandon the Task that has thrown the exception but carry on with the other Tasks. You can do this by creating a selective continuation with the OnlyOnFaulted value and calling the RemoveParticipant() method, which decreases the number of calls to SignalAndWait() that Barrier requires to mark the end of a phase. 
- This approach works as long as you can continue without the result that the abandoned Task would have otherwise provided