FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# Setup working directory
WORKDIR /src 

# Added arguments for NuGet
ARG NUGET_USERNAME
ARG NUGET_PASSWORD

COPY ["Pcf.Administration.WebHost/Pcf.Administration.WebHost.csproj", "Pcf.Administration.WebHost/"]
COPY ["Pcf.Administration.IntegrationTests/Pcf.Administration.IntegrationTests.csproj", "Pcf.Administration.IntegrationTests/"]
COPY ["Pcf.Administration.BLL/Pcf.Administration.BLL.csproj", "Pcf.Administration.BLL/"]
COPY ["Pcf.Administration.DataAccess/Pcf.Administration.DataAccess.csproj", "Pcf.Administration.DataAccess/"]
COPY ["Pcf.Administration.Core/Pcf.Administration.Core.csproj", "Pcf.Administration.Core/"]
COPY ["Pcf.Administration.Integration/Pcf.Administration.Integration.csproj", "Pcf.Administration.Integration/"]

# Copy NuGet.config in working directory
COPY NuGet.config .

RUN sed -i "s/%NUGET_USERNAME%/${NUGET_USERNAME}/g" NuGet.config \
    && sed -i "s/%NUGET_PASSWORD%/${NUGET_PASSWORD}/g" NuGet.config

## Restore dependecies
RUN dotnet restore "Pcf.Administration.WebHost/Pcf.Administration.WebHost.csproj"

# Remove NuGet.config from working directory
RUN rm NuGet.config

## Copy full solution over
COPY . .

## Build the app
RUN dotnet build "Pcf.Administration.WebHost/Pcf.Administration.WebHost.csproj" -c Release --no-restore

## Publish the app
FROM build AS publish
WORKDIR /src/Pcf.Administration.WebHost
RUN dotnet publish -c Release -o /app/publish --no-restore --no-build

## Run the app
FROM base AS final
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pcf.Administration.WebHost.dll"]
CMD ["--urls", "http://*:8080"]