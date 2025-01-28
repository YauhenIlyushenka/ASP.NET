FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Pcf.GivingToCustomer.WebHost/Pcf.GivingToCustomer.WebHost.csproj", "Pcf.GivingToCustomer.WebHost/"]
COPY ["Pcf.GivingToCustomer.IntegrationTests/Pcf.GivingToCustomer.IntegrationTests.csproj", "Pcf.GivingToCustomer.IntegrationTests/"]
COPY ["Pcf.GivingToCustomer.Integration/Pcf.GivingToCustomer.Integration.csproj", "Pcf.GivingToCustomer.Integration/"]
COPY ["Pcf.GivingToCustomer.BLL/Pcf.GivingToCustomer.BLL.csproj", "Pcf.GivingToCustomer.BLL/"]
COPY ["Pcf.GivingToCustomer.DataAccess/Pcf.GivingToCustomer.DataAccess.csproj", "Pcf.GivingToCustomer.DataAccess/"]
COPY ["Pcf.GivingToCustomer.Core/Pcf.GivingToCustomer.Core.csproj", "Pcf.GivingToCustomer.Core/"]

## Restore dependecies
RUN dotnet restore "Pcf.GivingToCustomer.WebHost/Pcf.GivingToCustomer.WebHost.csproj"

## Copy full solution over
COPY . .

## Build the app
RUN dotnet build "Pcf.GivingToCustomer.WebHost/Pcf.GivingToCustomer.WebHost.csproj" -c Release --no-restore

## Publish the app
FROM build AS publish
WORKDIR /src/Pcf.GivingToCustomer.WebHost
RUN dotnet publish -c Release -o /app/publish --no-restore --no-build

## Run the app
FROM base AS final
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pcf.GivingToCustomer.WebHost.dll"]
CMD ["--urls", "http://*:8081"]