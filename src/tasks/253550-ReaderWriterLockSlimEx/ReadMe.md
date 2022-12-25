
# Declarative Synchronization

- The example is taken from [here](https://learn.microsoft.com/en-us/dotnet/api/system.threading.readerwriterlockslim).

- The System.Threading.ReaderWriterLockSlim class provides a convenient implementation of readerwriter locking that takes care of managing the locks. 

- You acquire and release the ReaderWriterLockSlim read lock by calling the EnterReadLock() and ExitReadLock() methods. Similarly, you acquire and release the write lock by calling EnterWriteLock and ExitWriteLock(). 

- The ReaderWriterLockSlim class only provides the synchronization primitives; it does not enforce the separation between read and write operations in your code. Its the developer who must be careful to avoid modifying shared data in a Task that has only acquired the read lock. 

- The example creates five Tasks that acquire the read lock, wait for one second, and then release the read lock, repeating this sequence until they are cancelled. As the read lock is acquired and released, a message is printed to the console, and this message shows the number of holders of the read lock, which is available by reading the CurrentReadCount property. 

- When you press the Enter key, the main application thread acquires the write lock, which is held for two seconds and then released. You can see from the following results that once the write lock has been requested, the number of Tasks holding the read lock starts to drop. This is because calls to EnterReadLock() will now wait until the writer lock has been released to ensure writer exclusivity.

- If you press Enter again, the main application thread releases the write lock, which allows the Tasks to continue their acquire/release sequence once more.
