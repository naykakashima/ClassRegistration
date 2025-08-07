FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy csproj and restore
COPY ClassRegistrationApplication2025.csproj ./
COPY ClassRegistrationApplication2025.sln ./
RUN dotnet restore

# Copy everything else
COPY . .

# Publish the app
RUN dotnet publish ClassRegistrationApplication2025.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 5000

ENTRYPOINT ["dotnet", "ClassRegistrationApplication2025.dll"]
