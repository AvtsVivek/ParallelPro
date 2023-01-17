

# Manual Reset Event

- This demos Reset() method.
- 
- Reference. https://learn.microsoft.com/en-us/dotnet/api/system.threading.manualreseteventslim
- 
- manualResetEvent.Wait(); will block a thread.
- And manualResetEvent.Set(); will give signal so that the blocked threads will start. 
- Once Set(), all of the Waits downstream will not block.
- Then calling Reset() will cause the Wait() to block again.
