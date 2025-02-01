FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8083

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# Setup working directory
WORKDIR /src 

# Added arguments for NuGet
ARG NUGET_USERNAME
ARG NUGET_PASSWORD

COPY ["Pcf.ReceivingFromPartner.WebHost/Pcf.ReceivingFromPartner.WebHost.csproj", "Pcf.ReceivingFromPartner.WebHost/"]
COPY ["Pcf.ReceivingFromPartner.Business/Pcf.ReceivingFromPartner.Business.csproj", "Pcf.ReceivingFromPartner.Business/"]
COPY ["Pcf.ReceivingFromPartner.DataAccess/Pcf.ReceivingFromPartner.DataAccess.csproj", "Pcf.ReceivingFromPartner.DataAccess/"]
COPY ["Pcf.ReceivingFromPartner.Integration/Pcf.ReceivingFromPartner.Integration.csproj", "Pcf.ReceivingFromPartner.Integration/"]
COPY ["Pcf.ReceivingFromPartner.UnitTests/Pcf.ReceivingFromPartner.UnitTests.csproj", "Pcf.ReceivingFromPartner.UnitTests/"]
COPY ["Pcf.ReceivingFromPartner.Core/Pcf.ReceivingFromPartner.Core.csproj", "Pcf.ReceivingFromPartner.Core/"]

# Copy NuGet.config in working directory
COPY NuGet.config .

RUN sed -i "s/%NUGET_USERNAME%/${NUGET_USERNAME}/g" NuGet.config \
    && sed -i "s/%NUGET_PASSWORD%/${NUGET_PASSWORD}/g" NuGet.config

## Restore dependecies
RUN dotnet restore "Pcf.ReceivingFromPartner.WebHost/Pcf.ReceivingFromPartner.WebHost.csproj"

# Remove NuGet.config from working directory
RUN rm NuGet.config

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