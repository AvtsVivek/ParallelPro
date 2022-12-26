
- The System.Threading.ReaderWriterLockSlim class provides a convenient implementation of readerwriter locking that takes care of managing the locks. 

- You acquire and release the ReaderWriterLockSlim read lock by calling the EnterReadLock() and ExitReadLock() methods. Similarly, you acquire and release the write lock by calling EnterWriteLock and ExitWriteLock(). 

- The ReaderWriterLockSlim class only provides the synchronization primitives; it does not enforce the separation between read and write operations in your code. Its the developer who must be careful to avoid modifying shared data in a Task that has only acquired the read lock. 

- Demos how to acquire multiple reader locks

- A lock for a critical area is acquired by one Task/Thread. But by using ReaderWriterLockSlim you can have multiple tasks acquire lock over a single critical area. 

- Read locks are non exclusive locsk.
