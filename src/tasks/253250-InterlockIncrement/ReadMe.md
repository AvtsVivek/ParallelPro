# Interlocked Introduction

- The System.Threading.Interlocked class provides a set of static methods that use special features of the
operating system and hardware to provide high-performance synchronized operations. All of the
methods in Interlocked are static and synchronized.

- The Add(), Increment(), and Decrement() methods are convenient shortcuts when using integers

- The example shows how we can use Interlocked.Increment() to fix the data race

- Notice that we have had to change the SimpleClass to expose the counter as an integer, because Interlocked methods require arguments modified by the ref keyword and values from properties cannot be used with ref.

```cs
class SimpleClass
{
    public int Counter = 0;
}
```