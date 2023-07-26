# web-API-practice
Hands-On ASP.NET Core Web API - Build API from Scratch

# Helpful Links
- [Startup.cs class is missing in .NET 6 - Stack Overflow](https://stackoverflow.com/questions/70952271/startup-cs-class-is-missing-in-net-6)
- [ASP.NET 6 + Identity + Sqlite, services.AddDbContext() how? - Stack Overflow](https://stackoverflow.com/questions/69472240/asp-net-6-identity-sqlite-services-adddbcontext-how)
- [App startup in ASP.NET Core | Microsoft Learn](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/startup?view=aspnetcore-7.0)

# Notes on Versioning Web APIs
## Options
- No versioning
- One method is URL Versioning, such as /v2/courses
- There is also Query String Versioning, such as /courses?ver=2
- Header versioning, such as X-API-Version=2

## API Versioning Service
[Microsoft.AspNetCore.Mvc.Versioning](https://www.nuget.org/packages/Microsoft.AspNetCore.Mvc.Versioning/#readme-body-tab)
- By default, uses query param such as ../courses?api-version=1.0
- Can set default under Program.cs to use without query
