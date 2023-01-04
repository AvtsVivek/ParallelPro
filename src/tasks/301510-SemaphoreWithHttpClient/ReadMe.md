

- SemaphoreSlim is like a box of keys. Keys are for a room like conf or bath room. 
- If you want to use the room, then you need a key.
- If there is no key in the box, you have to wait for the key to go use the room. 
- So here we creating a SemaphoreSlim, we are creating a box with 1 keys in it.
- In the following example, we are bombarding the url(last parameter) and there can be an exception.
- But if we use SemaphoreSlim, then we can regulate the bombardment and then all the calls will be successifull.
- The first param is the number of tasks or the number of calls to the url(last param)
- The second param is the timeout in seconds.
- The third param is weather to use semaphore or not. 
- The shorter the timeout, the more likely the call to the url can throw an exception.
- The larger the first param(the number of calls), the more likely it can throw an exception.
- the last parm can be any we url, like google, youtube or bing etc. 


This is taken from the following.

[Working with Channels in .NET](https://www.youtube.com/watch?v=gT06qvQLtJ0)


