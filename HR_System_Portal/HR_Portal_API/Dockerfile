FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HR_Portal_API/HR_Portal_API.csproj", "HR_Portal_API/"]
RUN dotnet restore "HR_Portal_API/HR_Portal_API.csproj"
COPY . .
WORKDIR "/src/HR_Portal_API"
RUN dotnet build "HR_Portal_API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HR_Portal_API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HR_Portal_API.dll"]