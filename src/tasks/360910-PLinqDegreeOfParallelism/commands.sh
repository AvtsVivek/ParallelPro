cd ../../..

cd src/tasks/360910-PLinqDegreeOfParallelism

cd src/apps/360910-PLinqDegreeOfParallelism

dotnet run --project ./PLinqDegreeOfParallelism.csproj -- AsSequential 0

dotnet run --project ./PLinqDegreeOfParallelism.csproj -- AsParallel 0

dotnet run --project ./PLinqDegreeOfParallelism.csproj -- AsParallel 1

dotnet run --project ./PLinqDegreeOfParallelism.csproj -- AsParallel 2

dotnet run --project ./PLinqDegreeOfParallelism.csproj -- AsParallel 3



