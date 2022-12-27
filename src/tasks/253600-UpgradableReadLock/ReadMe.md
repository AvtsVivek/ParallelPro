
Acquiring the lock on a primitive when you already have a lock is called lock recursion. The
ReaderWriterLockSlim class doesn’t support lock recursion by default, because lock recursion has the
potential to create deadlocks. Instead, you should use an upgradable read lock, which allows you to read
the shared data, perform your test, and safely acquire exclusive write access if you need it. You acquire
and release an upgradable read lock by calling the EnterUpgradeableReadLock() and
ExitUpgradeableReadLock() methods and then acquire and release the write lock (if needed) by calling
the EnterWriteLock() and ExitWriteLock() as before.

Once the upgradable lock is acquired, requests for the write lock and further requests for the
upgradable read lock will block until ExitUpgradeableReadLock() is called, but multiple holders of the
read lock are allowed. Upgrading the lock by calling EnterWriteLock() waits for all of the current holders
of the read lock to call ExitReadLock() before the write lock is granted. Listing 3-16 demonstrates the use
of the upgradable read lock by having five Tasks that read shared data and two that use the upgradable
read lock to make changes.

The example demos Avoiding Lock Recursion by Using an Upgradable Read Lock
