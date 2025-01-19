FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8083

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Pcf.ReceivingFromPartner.WebHost/Pcf.ReceivingFromPartner.WebHost.csproj", "Pcf.ReceivingFromPartner.WebHost/"]
COPY ["Pcf.ReceivingFromPartner.Business/Pcf.ReceivingFromPartner.Business.csproj", "Pcf.ReceivingFromPartner.Business/"]
COPY ["Pcf.ReceivingFromPartner.DataAccess/Pcf.ReceivingFromPartner.DataAccess.csproj", "Pcf.ReceivingFromPartner.DataAccess/"]
COPY ["Pcf.ReceivingFromPartner.Integration/Pcf.ReceivingFromPartner.Integration.csproj", "Pcf.ReceivingFromPartner.Integration/"]
COPY ["Pcf.ReceivingFromPartner.UnitTests/Pcf.ReceivingFromPartner.UnitTests.csproj", "Pcf.ReceivingFromPartner.UnitTests/"]
COPY ["Pcf.ReceivingFromPartner.Core/Pcf.ReceivingFromPartner.Core.csproj", "Pcf.ReceivingFromPartner.Core/"]

## Restore dependecies
RUN dotnet restore "Pcf.ReceivingFromPartner.WebHost/Pcf.ReceivingFromPartner.WebHost.csproj"

## Copy full solution over
COPY . .

## Build the app
RUN dotnet build "Pcf.ReceivingFromPartner.WebHost/Pcf.ReceivingFromPartner.WebHost.csproj" -c Release --no-restore

## Publish the app
FROM build AS publish
WORKDIR /src/Pcf.ReceivingFromPartner.WebHost
RUN dotnet publish -c Release -o /app/publish --no-restore --no-build

## Run the app
FROM base AS final
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pcf.ReceivingFromPartner.WebHost.dll"]
CMD ["--urls", "http://*:8083"]