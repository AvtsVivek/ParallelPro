cd ../../..

cd src/tasks/360840-PLinqImmediateQueryExecution

cd src/apps/360840-PLinqImmediateQueryExecution

dotnet run --project ./PLinqImmediateQueryExecution.csproj -- AsSequential

dotnet run --project ./PLinqImmediateQueryExecution.csproj -- AsParallel

# Compare this with earlier example. 360810-PLinqDeferredQueryExecution

cd ../../..

cd src/tasks/360810-PLinqDeferredQueryExecution

cd src/apps/360810-PLinqDeferredQueryExecution

dotnet run --project ./PLinqDeferredQueryExecution.csproj -- AsSequential

dotnet run --project ./PLinqDeferredQueryExecution.csproj -- AsParallel

