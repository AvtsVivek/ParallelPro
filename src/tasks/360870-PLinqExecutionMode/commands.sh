cd ../../..

cd src/tasks/360870-PLinqExecutionMode

cd src/apps/360870-PLinqExecutionMode

dotnet run --project ./PLinqExecutionMode.csproj -- ForceParallelism

dotnet run --project ./PLinqExecutionMode.csproj -- Default

dotnet run --project ./PLinqExecutionMode.csproj -- AsSequential



