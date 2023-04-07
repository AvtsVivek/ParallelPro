cd ../../..

cd src/tasks/360590-ParallelExtensionKeyValuePair

cd src/apps/360590-ParallelExtensionKeyValuePair


dotnet run --project ./ParallelExtensionKeyValuePair.csproj -- AsSequential

# The following is sometimes giving excemption. NOt sure why.
dotnet run --project ./ParallelExtensionKeyValuePair.csproj -- AsParallel


