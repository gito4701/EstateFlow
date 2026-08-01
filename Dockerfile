FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["src/EstateFlow.Api/EstateFlow.Api.csproj", "src/EstateFlow.Api/"]
COPY ["src/EstateFlow.Application/EstateFlow.Application.csproj", "src/EstateFlow.Application/"]
COPY ["src/EstateFlow.Domain/EstateFlow.Domain.csproj", "src/EstateFlow.Domain/"]
COPY ["src/EstateFlow.Infrastructure/EstateFlow.Infrastructure.csproj", "src/EstateFlow.Infrastructure/"]
COPY ["src/EstateFlow.Shared/EstateFlow.Shared.csproj", "src/EstateFlow.Shared/"]
COPY ["EstateFlow.sln", "./"]

RUN dotnet restore EstateFlow.sln

COPY . .
RUN dotnet publish src/EstateFlow.Api/EstateFlow.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "EstateFlow.Api.dll"]
