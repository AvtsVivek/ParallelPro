
- This builds [from the example](https://github.com/AvtsVivek/ParallelPro/tree/main/src/apps/250350-CancellationWithPolling) and to run [look at this](https://github.com/AvtsVivek/ParallelPro/tree/main/src/tasks/250350-CancellationWithPolling). 

- Handling Exceptions in Tasks
- The AggregateException class provides the Handle()
method, which allows you to specify a function delegate that will be called for each exception. Your
function or lambda expression should return true if the exception is one that you can handle and false
otherwise.

- The objective of this example, is to look at the status of the task, after the cancellation. 
- We add a Task.WaitAll(task)( its same as task.Wait()), wrap this in a try catch block.
- Now you can see that the status is Cancelled. Note that we are passing the cancellationToken when creating the task. 
- If you do not pass that, then the status is faulted.
- So do the following
- Remove the following parameter

- Replace the following 
```cs
// create the task
var task = new Task(() => {
    ...
}, cancellationToken); // Remove this param, see below
```

- With the following 
```cs
// create the task
var task = new Task(() => {
    ...
});  // No token param here.
```
- So why is this parm needed? Take a look at [this ms docs](https://learn.microsoft.com/en-us/dotnet/standard/parallel-programming/task-cancellation)

```
When a task instance observes an OperationCanceledException thrown by the user code, it compares the exception's token to its associated token (the one that was passed to the API that created the Task). If the tokens are same and the token's IsCancellationRequested property returns true, the task interprets this as acknowledging cancellation and transitions to the Canceled state. 
```