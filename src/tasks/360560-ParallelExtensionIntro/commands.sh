cd ../../..

cd src/tasks/360560-ParallelExtensionIntro 

cd src/apps/360560-ParallelExtensionIntro


dotnet run --project ./ParallelExtensionIntro.csproj -- AsSequential


dotnet run --project ./ParallelExtensionIntro.csproj -- AsParallel

