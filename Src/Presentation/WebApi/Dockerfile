FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Src/Presentation/WebApi/WebApi.csproj", "Src/Presentation/WebApi/"]
COPY ["Src/Infrastructure/Infrastructure/Infrastructure.csproj", "Src/Infrastructure/Infrastructure/"]
COPY ["Src/Core/Common/Common.csproj", "Src/Core/Common/"]
COPY ["Src/Core/Application/Application.csproj", "Src/Core/Application/"]
COPY ["Src/Core/Domain/Domain.csproj", "Src/Core/Domain/"]
COPY ["Src/Infrastructure/Persistence/Persistence.csproj", "Src/Infrastructure/Persistence/"]
RUN dotnet restore "Src/Presentation/WebApi/WebApi.csproj"
COPY . .
WORKDIR "/src/Src/Presentation/WebApi"
RUN dotnet build "WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Trip.WebApi.dll"]