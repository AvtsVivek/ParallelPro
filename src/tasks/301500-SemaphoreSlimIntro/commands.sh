cd ../../..

cd src/tasks/301500-SemaphoreSlimIntro

cd src/apps/301500-SemaphoreSlimIntro

# semaphoreSlim is like a box of keys. Keys are for a room like conf or bath room. 
# If you want to use the room, then you need a key.
# If there is no key in the box, you have to wait for the key to go use the room. 
# So here we creating a SemaphoreSlim, we are creating a box with initialCount of keys in it.
# So here there are zero keys, so cannot do work
dotnet run --project ./SemaphoreSlimIntro.csproj -- RunZeroCountExample

# So here there is one keys, so CAN do work.
dotnet run --project ./SemaphoreSlimIntro.csproj -- RunOneCountExample

# So here there is one keys but 10 ppl, so only one CAN do work.
dotnet run --project ./SemaphoreSlimIntro.csproj -- RunOneKeyWithTenPeople

# So here there is one keys but 10 ppl. But each one will put back the key. So each person can access the room one after the other.
# We are putting back the key using Release.
dotnet run --project ./SemaphoreSlimIntro.csproj -- RunOneKeyWithTenPeopleWithRelease

# So here there are 10 keys and 10 ppl. So all can access the room.
dotnet run --project ./SemaphoreSlimIntro.csproj -- RunTenKeysWithTenPeople
