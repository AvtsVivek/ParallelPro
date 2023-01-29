

This is taken from the following.
https://www.youtube.com/watch?v=YxDORrTvIGM



- The first one is running sequentially.
- The second one RunWithTaskList calls the api parallelly and then gets the result sequentially. Note the following.
- This runs sequentially.
```cs
foreach (var task in taskList)
{
    DisplayCustomer(task.Result);
}
```
- The list output is also sequential, if you notice.
- The third one is truely parallel. The list output is not sequential. 
- It will be something like this. Note this is not sequential.
```txt
[1,2,3,4,5,6,7,8,9]
--------------
4: John Crichton
Friday, March 19, 1999      
Rating: *******
--------------
1: John Koenig
Friday, October 17, 1975    
Rating: ******
--------------
9: Isaac Gampu
Saturday, September 10, 1977
Rating: ****
--------------
```

- The fourth one is using channel.


