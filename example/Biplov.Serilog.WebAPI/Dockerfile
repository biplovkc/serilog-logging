#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /
COPY ["Biplov.Serilog.WebAPI/Biplov.Serilog.WebAPI.csproj", "Biplov.Serilog.WebAPI/"]
RUN dotnet restore "Biplov.Serilog.WebAPI/Biplov.Serilog.WebAPI.csproj"
COPY . .
WORKDIR "/Biplov.Serilog.WebAPI"
RUN dotnet build "Biplov.Serilog.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Biplov.Serilog.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Biplov.Serilog.WebAPI.dll"]