# OkMusic

## sqlite

```sh
# https://docs.microsoft.com/zh-cn/ef/core/get-started/overview/first-app?tabs=netcore-cli
dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Design

mkdir sqlite

dotnet ef migrations add InitialCreate
dotnet ef database update
```
