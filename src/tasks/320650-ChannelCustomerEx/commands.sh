cd ../../..

cd src/tasks/320650-ChannelCustomerEx

cd src/apps/320650-ChannelCustomerEx

dotnet run --project ./Customer.Service/Customer.Service.csproj

dotnet run --project ./Customer.Viewer/Customer.Viewer.csproj -- RunSequentially

dotnet run --project ./Customer.Viewer/Customer.Viewer.csproj -- RunWithContinuation

dotnet run --project ./Customer.Viewer/Customer.Viewer.csproj -- RunWithChannel

