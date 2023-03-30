FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ContentWriterService.csproj", "./"]
RUN dotnet restore "ContentWriterService.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "ContentWriterService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContentWriterService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "ContentWriterService.dll"]