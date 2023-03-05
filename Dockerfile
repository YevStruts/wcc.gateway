#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["wcc.gateway.api/wcc.gateway.api.csproj", "wcc.gateway.api/"]
COPY ["wcc.gateway.kernel/wcc.gateway.kernel.csproj", "wcc.gateway.kernel/"]
COPY ["wcc.gateway.data/wcc.gateway.data.csproj", "wcc.gateway.data/"]
COPY ["wcc.gateway.integrations/wcc.gateway.integrations.csproj", "wcc.gateway.integrations/"]
COPY ["wcc.gateway/wcc.gateway.csproj", "wcc.gateway/"]
RUN dotnet restore "wcc.gateway.api/wcc.gateway.api.csproj"
COPY . .
WORKDIR "/src/wcc.gateway.api"
RUN dotnet build "wcc.gateway.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "wcc.gateway.api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "wcc.gateway.api.dll"]