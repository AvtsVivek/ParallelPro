- Getting the result from task

- To get a result from a Task, create instances of Task<T>, where T is the type of the result that will be
produced and return an instance of that type in your Task body. To read the result, you call the Result
property of the Task you created

- Reading the Result property waits until the Task it has been called on has completed
- 

