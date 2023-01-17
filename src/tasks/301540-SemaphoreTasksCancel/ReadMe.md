

- The System.Threading.SemaphoreSlim class allows you to specify how many waiting worker Tasks are released when the event is set, which is useful when you want to restrict the degree of concurrency among a group of Tasks. 
- The supervisor releases workers by calling the Release() method. 
- The default version releases one Task, and you can specify how many Tasks are released by providing an integer argument. 
- The constructor requires that you specify how many calls to the Wait() method can be made before the event is reset for the first time. 
- Specifying 0 resets the event immediately, and any other value sets the event initially and then allows the specified number of calls to the Wait() method to be made without blocking before the event is reset.
- 
- Demos the use of this class. Ten worker Tasks are created and call the SemaphoreSlim.Wait() method. 
- A supervisor Task periodically releases two threads by signaling the primitive by calling SemaphoreSlim.Release(2).
- 