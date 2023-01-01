
# Orphaned Locks
- The .NET synchronization primitives require you to explicitly acquire and release the lock. 
- An orphaned lock is one that has been acquired but, because of an exception or poor programming, will never be released.
- Because the lock is never released, any Tasks that try to acquire the lock will wait indefinitely.

Solution

- Ensure that you do not return from a method before releasing a lock and handle exceptions by releasing your lock in the finally block of a try, catch, finally sequence. 
 
- Alternatively, use the lock keyword (although this may offer poor performance compared with one of the lightweight synchronization primitives).

Example

- The following example uses a Mutex to demonstrate an orphaned lock. The first Task created repeatedly acquires and releases the Mutex. The second Task acquires the Mutex and then throws an exception, so the Mutex is never released. The first Task deadlocks waiting to acquire the Mutex and cannot move forward. 

- The Mutex remains orphaned even when the second Task has finished and its exception has been processed.

- 