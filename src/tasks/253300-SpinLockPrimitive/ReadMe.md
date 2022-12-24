# Interlocked Spin lock 


Typically, when waiting to acquire a regular synchronization primitive, your Task is taken out of the schedule waits until it has acquired the primitive and can run again. 

Spinning takes a different approach; the Task enters a tight execution loop, periodically trying to acquire the primitive.

Spinning avoids the overhead of rescheduling the Task because it never stops running, but it doesn't allow another Task to take its place. 

Spinning is useful if you expect the wait to acquire the primitive to be very short.

The System.Threading.Spinlock class is a lightweight, spin-based synchronization primitive. It has a similar structure to other primitives in that it relies on Enter(), TryEnter(), and Exit() methods to acquire and release the lock. Listing 3-10 shows the bank account example implemented using spinLock.

The constructor for SpinLock has an overload that enables or disables owner tracking, which simply means that the primitive keeps a record of which Task has acquired the lock. 

SpinLock doesn't support recursive locking, so if you have already acquired the lock, you must not try to acquire it again. If you have enabled owner tracking, attempting recursive locking will cause a System.Threading.LockRecursionException to be thrown. If you have disabled owner tracking and try to lock recursively, a deadlock will occur. 


