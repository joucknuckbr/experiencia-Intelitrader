FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CadastroApi/CadastroApi.csproj", "CadastroApi/"]
RUN dotnet restore "CadastroApi/CadastroApi.csproj"
COPY . .
WORKDIR "/src/CadastroApi"
RUN dotnet build "CadastroApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CadastroApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CadastroApi.dll"]