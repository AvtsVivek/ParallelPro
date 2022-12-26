
- Demos how to acquire multiple reader locks along with a Writer lock

- Using ReaderWriterLockSlim you can have multiple tasks acquire lock over a single critical area.

- The example creates five Tasks that acquire the read lock, wait for one second, and then release the read lock, repeating this sequence until they are cancelled. As the read lock is acquired and released, a message is printed to the console, and this message shows the number of holders of the read lock, which is available by reading the CurrentReadCount property. 

- When you press the Enter key, the main application thread acquires the write lock, then when again press enter key, the writer is released. You can see from the following results that once the write lock has been requested, the number of Tasks holding the read lock starts to drop. This is because calls to EnterReadLock() will now wait until the writer lock has been released to ensure writer exclusivity. 

- Read locks are **non-exclusive** locks, while write locks are exclusive locks.
