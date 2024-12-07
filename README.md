# AssetTrackingEF
Asset tracking system implemented in C# using Entity Framework and SQL Server.<p>

## How to run the application

On Windows: Requires installation of SQL Server, I have used SQL Express:<br>
https://www.microsoft.com/en-us/sql-server/sql-server-downloads<p>

On Mac, there is a Docker image, so install Docker and then SQL Server Docker Image:<br>
sudo docker pull mcr.microsoft.com/mssql/server:2022-latest<p>

In the project folder I have used the dotnet command line tools for package installation:<br>
dotnet add package Microsoft.EntityFrameworkCore<br>
dotnet add package Microsoft.EntityFrameworkCore.SqlServer<br>
dotnet add package Microsoft.EntityFrameworkCore.Tools<p>

And command line tools installed globally:<br>
dotnet tool install --global dotnet-ef

Database can then be initialized using:<br>
dotnet ef update database

After that the app can be started using:<br>
dotnet run


