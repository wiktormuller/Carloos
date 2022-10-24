# Get Base Image (Full .NET Core SDK)
FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build-env
WORKDIR /app
EXPOSE 8080

# Copy csproj and restore
COPY JobJetRestApi.sln ./
COPY JobJetRestApi.Web/*.csproj ./JobJetRestApi.Web/
COPY JobJetRestApi.Application/*.csproj ./JobJetRestApi.Application/
COPY JobJetRestApi.Infrastructure/*.csproj ./JobJetRestApi.Infrastructure/
COPY JobJetRestApi.Domain/*.csproj ./JobJetRestApi.Domain/

RUN dotnet restore

# Copy everything else and build
COPY JobJetRestApi.Web/. ./JobJetRestApi.Web/
COPY JobJetRestApi.Application/. ./JobJetRestApi.Application/
COPY JobJetRestApi.Infrastructure/. ./JobJetRestApi.Infrastructure/
COPY JobJetRestApi.Domain/. ./JobJetRestApi.Domain/

WORKDIR /app/JobJetRestApi.Web
RUN dotnet publish -c Release -o out

# Generate run image
FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS runtime
WORKDIR /app

EXPOSE 5002
EXPOSE 5003
COPY --from=build-env /app/JobJetRestApi.Web/out .
ENTRYPOINT ["dotnet", "JobJetRestApi.Web.dll"]

# Argument for seeding data
CMD ["/seed"]