# Usare l'immagine base .NET per runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5001
# installazione di alcuni tool di rete
RUN apt update && apt install -y net-tools curl && rm -rf /var/lib/apt/lists/*

# Usare l'immagine per build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# copia di alcuni file all'interno del conteiner
COPY ["BookingService.Api/BookingService.Api.csproj", "BookingService.Api/"]
COPY ["BookingService.Business/BookingService.Business.csproj", "BookingService.Business/"]
COPY ["BookingService.Repository/BookingService.Repository.csproj", "BookingService.Repository/"]
COPY ["BookingService.Shared/BookingService.Shared.csproj", "BookingService.Shared/"]
COPY ["BookingService.ClientHttp/BookingService.ClientHttp.csproj", "BookingService.ClientHttp/"]
RUN dotnet restore "BookingService.Api/BookingService.Api.csproj"
COPY . .
WORKDIR "/src/BookingService.Api"
# Ripristina tutte le dipendenze dei progetti copiati
RUN dotnet build "BookingService.Api.csproj" -c Release -o /app/build

# Pubblicare l'app
FROM build AS publish
RUN dotnet publish "BookingService.Api.csproj" -c Release -o /app/publish

# Eseguire l'app
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookingService.Api.dll"]
