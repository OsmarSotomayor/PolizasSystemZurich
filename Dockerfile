# =========================
# Etapa 1: Build
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar archivos del proyecto
COPY . .

# Restaurar dependencias
RUN dotnet restore "PolizasSystemZurich/PolizasSystemZurich.csproj"

# Publicar en modo Release
RUN dotnet publish "PolizasSystemZurich/PolizasSystemZurich.csproj" -c Release -o /app/publish

# =========================
# Etapa 2: Runtime
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copiar la app publicada desde la etapa anterior
COPY --from=build /app/publish .

# Puerto expuesto para Azure (80)
EXPOSE 80

# Iniciar la aplicación
ENTRYPOINT ["dotnet", "PolizasSystemZurich.dll"]
