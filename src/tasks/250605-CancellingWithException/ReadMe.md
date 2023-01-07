
- Handling Exceptions in Tasks
- The AggregateException class provides the Handle()
method, which allows you to specify a function delegate that will be called for each exception. Your
function or lambda expression should return true if the exception is one that you can handle and false
otherwise.