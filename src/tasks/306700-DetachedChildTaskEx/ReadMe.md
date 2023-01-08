Check And propagate approach

- Each continuation Task check the status of the antecedent and handle the exception. 
- You can rethrow the same exception to propagate it throughout the continuation chain. 
- Doing so will cause all of the tasks in a chain to be scheduled and executed, but it does reduce the chance of an unhandled exception. 
- This demo this checkand-propagate approach.
- By having the continuation process the exceptions thrown by the antecedent, we can catch exceptions thrown by the last Task in the chain and be sure to avoid unhandled exceptions. 
- Note that the Exception property of an antecedent returns an instance of AggregateException. 
- The InnerException property is read in the continuation Task to avoid nested instances of AggregateException, unpacking the nesting in the exception handler would also work. 
- 