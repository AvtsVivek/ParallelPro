
- Let the task wait for a given amount of time. Specify a time period, to the WaitOne() method and it will put the task to sleep for the specific
number of milliseconds or until the CancellationToken is cancelled, whichever happens first.

- You can use Thread.Sleep() in place of bool cancelled = cancellationToken.WaitHandle.WaitOne(3000);
- The key difference with this technique is that cancelling the token doesn’t immediately cancel the
task, because the Thread.Sleep() method will block until the time specified has elapsed and only then
check the cancellation status. This means that the task continues to exist, albeit
asleep, for up to n seconds after the token has been cancelled.

- Thread.SpinWait() can also be used. But its not recommended.
- When you use the other two sleep techniques, the thread that is performing your task gives up its turn
in the schedule when its sleeping, so any other threads can have a turn. The scheduler, which is
responsible for managing the threads running at any given time, has to do some work to determine
which thread should go next and make it happen. You can avoid the scheduler having to do this work by
using a technique called spin waiting: the thread doesn’t give up its turn; it just enters a very tight loop
on the CPU


