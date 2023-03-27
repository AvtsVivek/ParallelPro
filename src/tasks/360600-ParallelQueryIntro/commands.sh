cd ../../..

cd src/tasks/360600-ParallelQueryIntro 

cd src/apps/360600-ParallelQueryIntro


dotnet run --project ./ParallelQueryIntro.csproj -- AsSequential


dotnet run --project ./ParallelQueryIntro.csproj -- AsParallel

