# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copy the project file and restore dependencies
COPY . .
RUN dotnet restore

# Copy the project files and build the application
RUN dotnet publish -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Expose the port
EXPOSE 80

# Set the entry point for the container
ENTRYPOINT ["dotnet", "crud-dotnet.dll"]
