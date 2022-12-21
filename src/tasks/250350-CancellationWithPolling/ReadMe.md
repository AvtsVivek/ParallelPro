
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

