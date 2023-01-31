# For each 

- One of the limitations of Parallel.For is that all index values are processed, making it impossible to create a parallel equivalent of a sequential loop such as this one, which increments the loop counter by two per iteration to create a stepped index:

```cs
for (int i = 0; i < 10; i +=2) {
    //...loop body...
}
```

- Fortunately, we can achieve the same effect by using Parallel.ForEach() and creating an IEnumerable that contains only the stepped values, as illustrated.