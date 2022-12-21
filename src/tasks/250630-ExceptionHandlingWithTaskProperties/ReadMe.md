
- An alternative to catching the exceptions is to use the properties of the Task class, in particular, the
IsCompleted, IsFaulted, IsCancelled, and Exception properties. You still have to catch
AggregateException when you call any of the trigger methods, but you can use the properties to
determine if a task has completed, thrown an exception, or been cancelled, and if an exception was
thrown, you can get the details of the exception