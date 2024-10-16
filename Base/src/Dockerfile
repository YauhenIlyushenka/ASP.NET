FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["PromoCodeFactory.WebHost/PromoCodeFactory.WebHost.csproj", "PromoCodeFactory.WebHost/"]
COPY ["PromoCodeFactory.UnitTests/PromoCodeFactory.UnitTests.csproj", "PromoCodeFactory.UnitTests/"]
COPY ["PromoCodeFactory.BusinessLogic/PromoCodeFactory.BusinessLogic.csproj", "PromoCodeFactory.BusinessLogic/"]
COPY ["PromoCodeFactory.DataAccess/PromoCodeFactory.DataAccess.csproj", "PromoCodeFactory.DataAccess/"]
COPY ["PromoCodeFactory.Core/PromoCodeFactory.Core.csproj", "PromoCodeFactory.Core/"]

## Restore dependecies
RUN dotnet restore "PromoCodeFactory.WebHost/PromoCodeFactory.WebHost.csproj"

## Copy full solution over
COPY . .

## Build the app
RUN dotnet build "PromoCodeFactory.WebHost/PromoCodeFactory.WebHost.csproj" -c Release --no-restore

## Run the unit tests separately from building the full Docker image, however it will run within Docker
### Create a new extra build target called testrunner
#FROM build AS testrunner
#WORKDIR /src/PromoCodeFactory.UnitTests
### When we run this build target it will run the unit tests only
#CMD ["dotnet", "test"]

## Publish the app
FROM build AS publish
WORKDIR /src/PromoCodeFactory.WebHost
RUN dotnet publish -c Release -o /app/publish --no-restore --no-build

## Run the app
FROM base AS final
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PromoCodeFactory.WebHost.dll"]
CMD ["--urls", "http://*:8080"]