FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HR_WorkFlow_API/HR_WorkFlow_API.csproj", "HR_WorkFlow_API/"]
RUN dotnet restore "HR_WorkFlow_API/HR_WorkFlow_API.csproj"
COPY . .
WORKDIR "/src/HR_WorkFlow_API"
RUN dotnet build "HR_WorkFlow_API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HR_WorkFlow_API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HR_WorkFlow_API.dll"]