
- Let the task wait for a given amount of time. Specify a time period, to the WaitOne() method and it will put the task to sleep for the specific
number of milliseconds or until the CancellationToken is cancelled, whichever happens first.

- You can use Thread.Sleep() in place of bool cancelled = cancellationToken.WaitHandle.WaitOne(3000);
- The key difference with this technique is that cancelling the token doesn’t immediately cancel the
task, because the Thread.Sleep() method will block until the time specified has elapsed and only then
check the cancellation status. This means that the task continues to exist, albeit
asleep, for up to n seconds after the token has been cancelled.




