FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ./WebApiService ./WebApiService
COPY ./BaseRepository ./BaseRepository
COPY ./AuthenticationBase ./AuthenticationBase
WORKDIR ./WebApiService
RUN dotnet restore "WebApiService.csproj"
RUN dotnet build "WebApiService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish  "WebApiService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApiService.dll"]