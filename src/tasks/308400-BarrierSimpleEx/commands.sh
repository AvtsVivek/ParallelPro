cd ../../..

cd src/tasks/308400-BarrierSimpleEx

cd src/apps/308400-BarrierSimpleEx

# The first parameter is participantCount and the second one is taskCount
# both must be equal in number. 

# This will work.
dotnet run --project ./BarrierSimpleEx.csproj 3 3

# This will also work.
dotnet run --project ./BarrierSimpleEx.csproj 4 4

# This does not work. This will be stuck. Four participants are expected and we are creating only 3 tasks.
# Each participant is a task. 
dotnet run --project ./BarrierSimpleEx.csproj 4 3

# This will throw exception
# The number of threads using the barrier exceeded the total number of registered participants
dotnet run --project ./BarrierSimpleEx.csproj 4 5
