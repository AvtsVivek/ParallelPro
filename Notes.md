- Background Thread and Foreground Thread
  - A managed thread created as a foreground thread is not the UI thread or the main thread. 
  - Foreground threads are threads that will prevent the managed process from terminating if they are running. 
  - If an application is terminated, any running background threads will be stopped so that the process can shut down.
  - By default, newly created threads are foreground threads

- A managed thread can be uniquely identified using the **ManagedThreadId** property of the Thread object. 
  - This property is an integer that is guaranteed to be unique across all threads and will not change over time.
  - asdf

- The **ThreadState** property is a read-only property that provides the [current execution state of the Thread object](https://learn.microsoft.com/en-us/dotnet/api/system.threading.threadstate).

- We see both [Task.Delay](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.delay) as well as [Thread.Sleep](https://learn.microsoft.com/en-us/dotnet/api/system.threading.thread.sleep) methods. So whats the difference?
  - [Use Thread.Sleep when you want to block the current thread](https://stackoverflow.com/a/20084603/1977871).
  - Use await Task.Delay when you want a logical delay without blocking the current thread.
  - [The biggest difference between Task.Delay and Thread.Sleep is that Task.Delay is intended to run asynchronously](https://stackoverflow.com/a/28413138/1977871). It does not make sense to use Task.Delay in synchronous code. It is a VERY bad idea to use Thread.Sleep in asynchronous code.

- 