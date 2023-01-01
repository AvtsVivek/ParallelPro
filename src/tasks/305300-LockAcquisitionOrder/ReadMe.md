
# References

Lock Acquisition Order
You may run into a lock acquisition order issue if you acquire multiple locks in a nested code block but
do so in a different order in two or more critical regions. The following fragment demonstrates a nested
block:
```cs
lock (lockObj1) {
    lock (lockObj2) {
    ...critical region...
    }
}
```
If you repeat this nested acquisition to protect another critical region but don’t acquire the locks in
the same order (i.e., acquire lockObj2 and then lockObj1), you create a potential deadlock where neither
of the two Tasks (or any other Task that will wish to acquire either lock) can continue.

If you are using wait-handle–based primitives, the best solution is to use the WaitAll() method

This method will acquire the lock on multiple wait handles in a single step, which avoids the deadlock.

If you are using another kind of primitive, the only solution is to ensure that you always acquire the
locks in the same order. A simple trick is to name the primitive instances sequentially (lock1, lock2, etc.)
and always acquire the lowest ordered name first. Ensuring that you don’t duplicate primitive instance
names in your code is a lot easier than debugging a deadlock.
