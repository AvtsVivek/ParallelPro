
The following will indicate that writing is done.

```cs
boundedChannel.Writer.Complete();
```

The the following loop will exit.

```cs

// So here, once the Complete() is called, the following loop will exit.
//await foreach (var item in boundedChannel.Reader.ReadAllAsync())
//{
//    Console.WriteLine(item);
//}

// We can use the above loop or below loop. Any one will work.

while (await boundedChannel.Reader.WaitToReadAsync())
{
    Console.WriteLine(await boundedChannel.Reader.ReadAsync());
}

```
This is taken from the following.

[Working with Channels in .NET](https://www.youtube.com/watch?v=gT06qvQLtJ0)



