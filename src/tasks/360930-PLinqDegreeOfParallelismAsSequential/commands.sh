cd ../../..

cd src/tasks/360930-PLinqDegreeOfParallelismAsSequential

cd src/apps/360930-PLinqDegreeOfParallelismAsSequential

dotnet run --project ./PLinqDegreeOfParallelismAsSequential.csproj -- AsSequential 0

dotnet run --project ./PLinqDegreeOfParallelismAsSequential.csproj -- AsParallel 0

dotnet run --project ./PLinqDegreeOfParallelismAsSequential.csproj -- AsParallel 1

dotnet run --project ./PLinqDegreeOfParallelismAsSequential.csproj -- AsParallel 2

dotnet run --project ./PLinqDegreeOfParallelismAsSequential.csproj -- AsParallel 3



