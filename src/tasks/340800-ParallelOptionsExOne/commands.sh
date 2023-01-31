cd ../../..

cd src/tasks/340800-ParallelOptionsExOne

cd src/apps/340800-ParallelOptionsExOne

# - MaxDegreeOfParallelism: Get or set the maximum concurrency for a parallel loop. Setting this property to -1 means no concurrency limit is set

dotnet run --project ./ParallelOptionsExOne.csproj -- -1

dotnet run --project ./ParallelOptionsExOne.csproj -- 1

