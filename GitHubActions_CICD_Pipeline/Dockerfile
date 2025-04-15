# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers for better caching
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the source code
COPY . ./
RUN dotnet publish -c Release -o out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Set environment to Production for deployment
ENV ASPNETCORE_ENVIRONMENT=Production

# Expose the standard HTTP port
EXPOSE 8080

# Copy published output from build stage
COPY --from=build-env /app/out ./

ENTRYPOINT ["dotnet", "GitHubActions_CICD_Pipeline.dll"]

