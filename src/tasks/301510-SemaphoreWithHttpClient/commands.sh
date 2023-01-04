cd ../../..

cd src/tasks/301510-SemaphoreWithHttpClient

cd src/apps/301510-SemaphoreWithHttpClient

# semaphoreSlim is like a box of keys. Keys are for a room like conf or bath room. 
# If you want to use the room, then you need a key.
# If there is no key in the box, you have to wait for the key to go use the room. 
# So here we creating a SemaphoreSlim, we are creating a box with initialCount of keys in it.
# So here there are zero keys, so cannot do work
dotnet run --project ./SemaphoreWithHttpClient.csproj -- 30 6 false https://youtube.com

dotnet run --project ./SemaphoreWithHttpClient.csproj -- 30 6 true https://youtube.com


