FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ProSoft.UI/ProSoft.UI.csproj", "ProSoft.UI/"]
COPY ["ProSoft.Core/ProSoft.Core.csproj", "ProSoft.Core/"]
COPY ["ProSoft.EF/ProSoft.EF.csproj", "ProSoft.EF/"]
COPY ["Shared/ProSoft.Shared.csproj", "Shared/"]
RUN dotnet restore "ProSoft.UI/ProSoft.UI.csproj"
COPY . .
WORKDIR "/src/ProSoft.UI"
RUN dotnet build "ProSoft.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProSoft.UI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProSoft.UI.dll"]
