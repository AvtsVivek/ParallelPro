- This is a modification of earlier example. 
- Here the expected value is 10000 but the actual value is differnet. Run and see.
- Why is that?
- Although there are 10 tasks here, the number of threads executing them may be less.
- So one thread could be used to execute multiple tasks. When that is the case, the threadLocalStorage is shared. So its value increases.
- So may be that is the reason for this discrpency.
- Now in the earlier example, we have the following. 

```cs
    // get the state object
    threadLocalStorage.Value = (int)stateObject!;
```

- So this sets the threadLocalStorage.Value to zero each time a new task is started. And so we get the correct expected in that case.

