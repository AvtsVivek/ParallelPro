cd ../../..

cd src/tasks/301510-SemaphoreWithHttpClient

cd src/apps/301510-SemaphoreWithHttpClient

# In the following example, we are bombarding the url(last parameter) and there can be an exception.
# But if we use SemaphoreSlim, then we can regulate the bombardment and then all the calls will be successifull.
# The first param is the number of tasks or the number of calls to the url(last param)
# The second param is the timeout in seconds.
# The third param is weather to use semaphore or not. 
# The shorter the timeout, the more likely the call to the url can throw an exception.
# The larger the first param(the number of calls), the more likely it can throw an exception.
# the last parm can be any we url, like google, youtube or bing etc. 
dotnet run --project ./SemaphoreWithHttpClient.csproj -- 30 6 false https://youtube.com

dotnet run --project ./SemaphoreWithHttpClient.csproj -- 30 6 true https://youtube.com


