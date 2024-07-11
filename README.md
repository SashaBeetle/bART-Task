# bART-Task
### Project for `'bART Solutions'` 
## Stack
* [.NET](https://dotnet.microsoft.com/) - free, open-source, cross-platform framework for building modern apps and powerful cloud services.
* [Postgres SQL](https://www.postgresql.org/) - is a powerful, open source object-relational database system with over 35 years of active development that has earned it a strong reputation for reliability, feature robustness, and performance.
* [Entity Framework](https://learn.microsoft.com/uk-ua/ef/) - object-relational mapping (ORM) framework for .NET developers that enables them to work with databases using .NET objects, simplifying the process of data access and manipulation.
* [AutoMapper](https://automapper.org/) - is a simple little library built to solve a deceptively complex problem - getting rid of code that mapped one object to another. 
* [NuGet packages](https://learn.microsoft.com/uk-ua/nuget/) - type of software package used in the Microsoft .NET ecosystem, containing compiled code and other resources, and are used by developers to easily add functionality to their projects and share code between teams.

## How to run Backend
Open your system terminal and run commands:
```sh
git clone https://github.com/SashaBeetle/bART-Task.git
```
Add your database connection string to files:
In `bART-Task\bART-Task\bART-Task.EF\DIConfiguration.cs` method `GetConnectionString()`.Method should look like that:
```sh
private static void RegisterDatabaseDependencies(this IServiceCollection services, IConfigurationRoot configuration)
{
    services.AddDbContext<bARTTaskContext>(options =>
        options.UseNpgsql(configuration.GetConnectionString("PostgreSQLDatabase")));
}
```
Add `User Secrets` and instead of `ConnectionString` add your database connection string. 
```sh
  "ConnectionStrings": {
    "PostgreSQLDatabase": "ConnectionString"
  }
```
It is necessary to make migration and update database.
In `bART-Task\bART-Task\bART-Task.EF` open the PMC(Package Manager Console) and then push the command:
```sh
Update-Database
```
