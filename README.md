# AssetTrackingEF
Asset tracking system implemented in C# using Entity Framework and SQL Server.<p>

## How to run the application

This run this application *.NET 8.0 SDK and Runtime*, *Entity Framework* and *SQL Server* has to be installed. 

**Windows**: Requires installation of SQL Server, I have used SQL Express:<br>
https://www.microsoft.com/en-us/sql-server/sql-server-downloads<p>

**Mac**: there is a Docker image with the SQL server available, so install *Docker* and then the *SQL Server Docker Image*:<p>
```sudo docker pull mcr.microsoft.com/mssql/server:2022-latest```<p>

In the project folder I have used the dotnet command line tools for package installation:<p>
```dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer<br>
dotnet add package Microsoft.EntityFrameworkCore.Tools```

And command line tools installed globally:<p>
```dotnet tool install --global dotnet-ef```

Database can then be initialized using:<p>
```dotnet ef update database```

After that the app can be started using:<p>
```dotnet run```


