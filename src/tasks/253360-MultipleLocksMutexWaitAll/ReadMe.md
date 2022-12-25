# Mutex Wait All.

- The WaitAll() method is inherited from the WaitHandle class and takes an array of WaitHandles as the set of locks to acquire. Notice that although you can acquire multiple locks in a single step, you must release them individually using the Mutex.ReleaseMutex() method. The WaitAny() method returns when any of the locks have been acquired, and it returns an int that tells you the position of the acquired lock in the WaitHandle array passed in as a parameter. 

- The WaitOne(), WaitAll(), and WaitAny() methods are all overridden so that you can attempt to acquire a lock or set of locks for a given period of time.

- Wait handles can be shared between processes. The Mutexes in this example is local, meaning that it is only usable in one process

- A local Mutex is created when you use the default constructor

- You can also create a named system Mutex, which is the kind that can be shared between processes. We will see that in subsequent example.

