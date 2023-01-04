
# Continuation Tasks

- One of the most useful features of the TPL is the flexible way you can coordinate what groups of Tasks do to build complex parallel programs. 

- The first two are task continuations and child tasks. 

- Continuations let you create chains of Tasks that are executed one after the other. 

- They allow you to chains of Tasks, which are performed in order, so when one Task in the chain finishes, the next one begins

- Child tasks are run in the body of another Task and let you break down work into smaller pieces in order to increase concurrency. This example demonstrates a simple task continuation.

- In the example, we are creating a simple continuation and it is a two step process:
  - Create a new Task instance.
  - Call the ContinueWith() method on the new Task, supplying a System.Action<Task> delegate as the method parameter.
- Performing these steps creates two Tasks. The first Task is called the antecedent. The second Task, returned by the ContinueWith() method, is called the continuation. 
- Once both Tasks have been created, calling the antecedent Start() method schedules the Task as usual and, when it has been completed, automatically **schedules** the continuation Task. 
- **schedules** means, some other task can potencially run between these two task. This is up to the Thread Schedular of the CLR, not in developers hands.

