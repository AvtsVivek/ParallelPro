# Manual Reset Event

- Reference. https://learn.microsoft.com/en-us/dotnet/api/system.threading.manualreseteventslim
- 
- manualResetEvent.Wait(); will block a thread.
- And manualResetEvent.Set(); will give signal so that the blocked threads will start. 
- Once Set(), all of the Waits downstream will not block.
- Note "Done!!!!" may not be executed. That thread or task will be terminated before.
