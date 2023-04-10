
Note the difference.

dotnet run --project ./PLinqExecutionMode.csproj -- ForceParallelism
ForceParallelism
ForceParallelism
Result 16
Result 36
Result 64
Result 0
Result 4

dotnet run --project ./PLinqExecutionMode.csproj -- Default
Default
Default  
Result 16
Result 36
Result 64
Result 0 
Result 4 

dotnet run --project ./PLinqExecutionMode.csproj -- AsSequential
AsSequential
Default
Result 0
Result 4
Result 16
Result 36
Result 64