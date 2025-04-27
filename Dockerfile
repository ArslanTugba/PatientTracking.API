# Stage 1: Build Environment (SDK)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy the .csproj file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy all source code and publish the application
COPY . ./
RUN dotnet publish -c Release -o out

# Stage 2: Runtime Environment (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out ./
ENTRYPOINT ["dotnet", "PatientTrackingAPI.dll"]