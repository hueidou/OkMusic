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

## 参考

* [ASP.NET Core SignalR](https://docs.microsoft.com/zh-cn/aspnet/core/signalr/introduction?view=aspnetcore-5.0)
* [Element Plus](https://element-plus.gitee.io/)
* [Vue v3](https://v3.vuejs.org/guide/introduction.html)
* [vitejs](https://github.com/vitejs/vite)
* [Fetch API](https://developer.mozilla.org/zh-CN/docs/Web/API/Fetch_API)
