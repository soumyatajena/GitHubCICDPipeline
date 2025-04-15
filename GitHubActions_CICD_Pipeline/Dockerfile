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
ENV ASPNETCORE_ENVIRONMENT=Production
COPY --from=build-env /app/out ./
EXPOSE 80
ENTRYPOINT ["dotnet", "GitHubActions_CICD_Pipeline.dll"]


