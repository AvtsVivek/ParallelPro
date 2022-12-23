
- The earlier example(252520-IsolationByConvention) is an example of isolation by convention: the code is written so that the Tasks
works in isolation but the isolation is not enforced by the .NET runtime; The developers have to take care to ensure
that Tasks don’t share data by mistake.

- On the other hand, .NET provides the System.Threading.ThreadLocal class, which creates isolation that is enforced by
the .NET Framework. 

- **ThreadLocal** represents a special kind of data called **thread local storage** (TLS), where a single ThreadLocal results in each thread that accesses the data getting its own isolated instance.

- When we declare a ThreadLocal, to hold, say a string, we make the following call:
```cs
ThreadLocal<string> isolatedData = new ThreadLocal<string>();
```

- Imagine that the .NET Framework creates Dictionary<Thread, string> behind the scenes and that whenever we call the ThreadLocal.Value property to get or set the isolated data value, the framework translates that into a query against the Dictionary so that we only read or write the value associated with the current thread and each thread has its own value.

- TLS doesn’t really use a Dictionary, but imagining that way is useful for understanding. 

- The important thing to remember is each thread has its own isolated data value and each thread can’t read or write the value belonging to any other thread.

- Now, notice that we are talking about threads and not Tasks, and remember that a single thread can be used to perform multiple Tasks. 
- To ensure that you get the results you expect with Tasks, make sure that you set the ThreadLocal.Value property at the start of your Task body. 
- The example shows ThreadLocal being used where a simple counter of an object, with the isolated data value being set at the start of the Task body.

- Even though only one instance of ThreadLocal<int> is created, each Task is able to initialize its own isolated instance of the data using the state object and perform updates without worrying about data races. 

- ThreadLocal provides an overloaded constructor so you can supply a factory delegate that will
initialize the isolated data value. 

- This factory delegate is lazily initialized, meaning that it will not be called until the Task calls the ThreadLocal.Value property for the first time.


