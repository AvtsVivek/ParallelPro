# Continuation Tasks

- This example builds from the previous example. The continuation Tasks in this example returns a value.
- In the previous example, it does not.
- Task.ContinueWith<T>() allows you to create continuation Tasks that return a result.
- Note that the return type *can differ* from the return tyep of antecedentTask.
- The return type of antecedentTask in this example is int.
- And the return type of continuationTask is double.

