FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["rock-paper-scissors/rock-paper-scissors.csproj", "rock-paper-scissors/"]
RUN dotnet restore "rock-paper-scissors/rock-paper-scissors.csproj"
COPY . .
WORKDIR "/src/rock-paper-scissors"
RUN dotnet build "rock-paper-scissors.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "rock-paper-scissors.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "rock-paper-scissors.dll"]
