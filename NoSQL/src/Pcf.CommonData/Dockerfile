FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8082

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Pcf.CommonData.WebHost/Pcf.CommonData.WebHost.csproj", "Pcf.CommonData.WebHost/"]
COPY ["Pcf.CommonData.Business/Pcf.CommonData.Business.csproj", "Pcf.CommonData.Business/"]
COPY ["Pcf.CommonData.DataAccess/Pcf.CommonData.DataAccess.csproj", "Pcf.CommonData.DataAccess/"]
COPY ["Pcf.CommonData.Core/Pcf.CommonData.Core.csproj", "Pcf.CommonData.Core/"]

## Restore dependecies
RUN dotnet restore "Pcf.CommonData.WebHost/Pcf.CommonData.WebHost.csproj"

## Copy full solution over
COPY . .

## Build the app
RUN dotnet build "Pcf.CommonData.WebHost/Pcf.CommonData.WebHost.csproj" -c Release --no-restore

## Publish the app
FROM build AS publish
WORKDIR /src/Pcf.CommonData.WebHost
RUN dotnet publish -c Release -o /app/publish --no-restore --no-build

## Run the app
FROM base AS final
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pcf.CommonData.WebHost.dll"]
CMD ["--urls", "http://*:8082"]