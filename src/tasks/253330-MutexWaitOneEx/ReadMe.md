# Mutex Wait one.

Wait handles are wrappers around a Windows feature called synchronization handles. Several .NET synchronization primitives that are based on wait handles, and they all derive from the System.Threading.WaitHandle class. 

Each class has slightly different characteristics, and we’ll walk through each of them in the next chapter when we explore coordinating Tasks.

The wait handle class that has most relevance to avoiding data races and is System.Threading.Mutex. 
The example shows the basic use of the Mutex class to solve the counter data race problem. You acquire the lock on Mutex by calling the WaitOne() method and release the lock by calling ReleaseMutex().