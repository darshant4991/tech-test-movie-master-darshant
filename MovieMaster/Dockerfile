FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["MovieMaster/MovieMaster.csproj", "MovieMaster/"]
RUN dotnet restore "MovieMaster/MovieMaster.csproj"
COPY . .
WORKDIR "/src/MovieMaster"
RUN dotnet build "MovieMaster.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MovieMaster.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MovieMaster.dll"]
