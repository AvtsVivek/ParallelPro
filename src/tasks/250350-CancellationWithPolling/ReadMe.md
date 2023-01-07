
Creating a Task that you can cancel is a four-step process:

1. Create a new instance of System.Threading.CancellationTokenSource:
CancellationTokenSource tokenSource = new CancellationTokenSource

2. Call the CancellationTokenSource.Token property to get a System.Threading.
CancellationToken:
CancellationToken token = tokenSource.Token;

3. Create a new Task or Task<T> using an Action or Action<object> delegate and
the CancellationToken from step 2 as constructor arguments:
Task task1 = new Task(new Action(myMethod), token);

4. Call the Start() method on your Task or Task<T> as you would normally.

- To cancel a Task, simply call the Cancel() method on the CancellationTokenSource created in step 1.

- You should throw an instance of the OperationCanceledException to acknowledge a cancellation request.

- Notice that we need to pass token as a parameter
```cs
Task task1 = new Task(new Action(myMethod), token);
```
- So why is this needed? Take a look at [this ms docs](https://learn.microsoft.com/en-us/dotnet/standard/parallel-programming/task-cancellation)

```
When a task instance observes an OperationCanceledException thrown by the user code, it compares the exception's token to its associated token (the one that was passed to the API that created the Task). If the tokens are same and the token's IsCancellationRequested property returns true, the task interprets this as acknowledging cancellation and transitions to the Canceled state. 
```

- If you run this example, then you will see the following
```
The status of the task is Running
```
- This is because at time of the execution of this pirticular console write line statement, the task status is actually running. 
- If you wnat to know the exact status, then you have to wait till the tasks completion, so you need to use something like, task.Wait(), or Task.WaitAll(task). But there is a catch here, in order to use this wait, we need to have try catch blocks. 
- So another example is created based on this same example, added excemption handling. 
- 