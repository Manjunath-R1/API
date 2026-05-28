



##Basic Commands
Build cmd - dotnet Build
Run cmd - dotnet run -p ./src/ThoughtFocus.App/ThoughtFocus.App.csproj
Test cmd- dotnet test
Install Entity Framework: dotnet tool install --global dotnet-ef

##Migration Commands
///////////////////
1. go to startup Project directory :
2. for update database  : dotnet ef database update --project ..\1-Core\ThoughtFocus.DataAccess --context ApplicationDBContext
3. for add migration : dotnet ef migrations add [migrationName] --project ..\1-Core\ThoughtFocus.DataAccess --context ApplicationDBContext
4. for Target Migration : dotnet ef database update [MigrationName]--project ..\1-Core\ThoughtFocus.DataAccess --context ApplicationDBContext

##Migration Commands for Workflow project
1. go to startup Project directory :.
2. for update database  : dotnet ef database update --project ..\1-Core\ThoughtFocus.Common.WorkFlowDataAccess --context WorkFlowContext
3. for add migration : dotnet ef migrations add [migrationName] --project ..\1-Core\ThoughtFocus.Common.WorkFlowDataAccess --context WorkFlowContext


##To create class library:
dotnet new classlib -o ThoughtFocus.Common.Exceptions

##Add class library into solution:
1. goto root directory : dotnet sln add .\ src\1-Core\ThoughtFocus.DataAccess\ThoughtFocus.DataAccess.csproj 

##Add class library reference into another class library:
dotnet add ./Test/ThoughtFocus.Tests/ThoughtFocus.Tests.csproj reference ./src/ThoughtFocus.App/ThoughtFocus.App.csproj.csproj




