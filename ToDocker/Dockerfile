# Dockerfile для ASP.NET Core приложения
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ToDocker/ToDocker.csproj", "ToDocker/"]
RUN dotnet restore "ToDocker/ToDocker.csproj"
COPY . .
WORKDIR "/src/ToDocker"
RUN dotnet build "ToDocker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ToDocker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToDocker.dll"]
