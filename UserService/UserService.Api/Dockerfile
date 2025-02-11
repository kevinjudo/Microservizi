# Usare l'immagine base .NET per runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000
# installazione di alcuni tool di rete
RUN apt update && apt install -y net-tools curl && rm -rf /var/lib/apt/lists/*

# Usare l'immagine per build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["UserService.Api/UserService.Api.csproj", "UserService.Api/"]
COPY ["UserService.Business/UserService.Business.csproj", "UserService.Business/"]
COPY ["UserService.Repository/UserService.Repository.csproj", "UserService.Repository/"]
COPY ["UserService.Shared/UserService.Shared.csproj", "UserService.Shared/"]
COPY ["UserService.ClientHttp/UserService.ClientHttp.csproj", "UserService.ClientHttp/"]
RUN dotnet restore "UserService.Api/UserService.Api.csproj"
COPY . .
WORKDIR "/src/UserService.Api"
# Ripristina tutte le dipendenze dei progetti copiati
RUN dotnet build "UserService.Api.csproj" -c Release -o /app/build

# Pubblicare l'app
FROM build AS publish
RUN dotnet publish "UserService.Api.csproj" -c Release -o /app/publish

# Eseguire l'app
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserService.Api.dll"]
