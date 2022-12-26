- If we want to read data and make a change only if some condition is met, then we could acquire the write lock to do this, but that write lock requires exclusive access. 

- And we may not know in advance if you actually need to make changes, that would be a potential performance problem.

- This demos a problem if we acquire the (nonexclusive) read lock, perform the test, and then acquire the (exclusive) write lock if we need to make modifications.

- In that case, we get an exception.

- Press enter to finish
Read lock acquired - count: 1. The current thread id is 4
Write lock may not be acquired with read lock held. This pattern is prone to deadlocks. Please ensure that read locks are released before taking a write lock. If an upgrade is necessary, use an upgrade lock in place of the read lock.
Read lock released - count: 0. The current thread id is 4

- Acquiring the lock on a primitive when you already have a lock is called **lock recursion**. 

- The ReaderWriterLockSlim class doesn’t support lock recursion by default, because lock recursion has the potential to create deadlocks. 

- Instead, we should use an upgradable read lock, which allows you to read the shared data, perform your test, and safely acquire exclusive write access if you need it. 

- You acquire and release an upgradable read lock by calling the EnterUpgradeableReadLock() and ExitUpgradeableReadLock() methods and then acquire and release the write lock (if needed) by calling the EnterWriteLock() and ExitWriteLock() as before. We will see that in the next example.

- 