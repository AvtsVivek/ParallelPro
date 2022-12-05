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

- Interruption of Threads
  - It is possible to pass Timeout.Infinite to Thread.Sleep. This will cause the thread to pause until it is interrupted 
by another thread or the managed environment.
  - Interrupting a blocked or paused thread is accomplished by calling Thread.Interrupt. When a thread is interrupted, it will receive a ThreadInterruptedException exception.
  - The exception handler should allow the thread to continue working or clean up any remaining work. 
  - If the exception is unhandled, the runtime will catch the exception and stop the thread. 
  - Calling Thread.Interrupt on a running thread will have no effect until that thread has been blocked.

- Synchronizing data across threads
  - In a multi threaded app, several threads run, and can potencially access a portion of code 'simultaneously'. This can lead to problems. For example considert he following bank transaction example. A thread is supposed checks the state of two accounts, ensures they are in appropriate state, and then deducts some amount from one account and adds amount to another account. Lets say while this thread has checked the states of the accounts and is about to do the transaction, another thread comes along and changes the states of the account into some in appropriate states for the transaction to happen. So now the first thread will complete the transaction but to accounts in in-appropriate states.
  - So to avoid such secerios, there should be some constructs which will NOT allows access to other threads while the first thread is doing its job. Enter Synchronizing data.
  - There are multiple ways of doing this.
  - Monitor class. This will allow only one thread to access the order class.
  ```cs
  Monitor.Enter(order);
  order.AddDetails(orderDetail);
  Monitor.Exit(order);
  ```
  - The other is lock keyword
  - Next is interlocked class.
  - Then there is Mutex class. 
  - Take a look at this. https://learn.microsoft.com/en-us/dotnet/standard/threading/overview-of-synchronization-primitives

- Thread Priority example. Take a look at this one. 300350-ThreadPriority

- Cancellation. You need to pass a CancellationTokenSource when the thread is being created. Then call the Cancel method when ever you want to cancel the thread. See the example 300375-ThreadCanceling

- Does the use of async/await create a new thread?
  - The answer is not clear.
  - See [this so question](https://stackoverflow.com/q/27265818/1977871) and [this one.](https://stackoverflow.com/questions/17661428/async-stay-on-the-current-thread)
  - Other than that [here is what Stephan Cleary says](https://blog.stephencleary.com/2013/11/there-is-no-thread.html)
  - But on the other side, the book says the following in Chapter 2 in the section C# 5 and 6 and .NET Framework 4.5.x
    - The Task object returns from an async operation, providing a way for developers to check the status of the operation or wait for its completion. 
    - The work of an async task is performed on a background thread on the thread pool, rather than in the main thread. 

- ThreadPool. For an example, take a look at 300400-ThreadPoolIntro
  - The key differences when it comes to ThreadPool as againest regular threads are
  - 1. there is no need to set IsBackground to true, and 
  - 2. you do not call Start(). The process will start either as soon as the item is queued on ThreadPool or when the next ThreadPool becomes available.

 - Timers. They raise events at given times or specific intervals. Take a look at the example 300450-TimersExamples
 
 - 