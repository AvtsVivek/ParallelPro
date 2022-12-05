- Background Thread and Foreground Thread
  - A managed thread created as a foreground thread is not the UI thread or the main thread. 
  - Foreground threads are threads that will prevent the managed process from terminating if they are running. 
  - If an application is terminated, any running background threads will be stopped so that the process can shut down.
  - By default, newly created threads are foreground threads

- A managed thread can be uniquely identified using the **ManagedThreadId** property of the Thread object. 
  - This property is an integer that is guaranteed to be unique across all threads and will not change over time.
  - asdf

- The **ThreadState** property is a read-only property that provides the [current execution state of the Thread object](https://learn.microsoft.com/en-us/dotnet/api/system.threading.threadstate).

- 