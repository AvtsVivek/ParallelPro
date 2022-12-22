- Excessive Spinning

- Many programmers overestimate the performance impact of a Task waiting (either via Thread.Sleep() or
by using a CancellationToken wait handle) and use spin waiting instead (through the Thread.SpinWait()
method or by entering a code loop).
For anything other than exceptionally short waits, spin waiting and code loops will cripple the
performance of your parallel program, because avoiding a wait also avoids freeing up a thread for
execution.

- Solution

- Restrict your use of spin waiting and code loops to situations where you know that the condition that
you are waiting for will take only a few CPU cycles. If you must avoid a full wait, use spin waiting in
preference to code loops.

- Example

- In the following example, one Task enters a code loop to await the cancellation of another Task. Another
Task does the same thing but uses spin waiting. On the quad-core machine that I used to write this book,
this example burns roughly 30 percent of the available CPU, which is quite something for a program that
does nothing at all. You may get different results if you have fewer cores.