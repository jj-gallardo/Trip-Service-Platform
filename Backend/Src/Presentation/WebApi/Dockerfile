FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Backend/Src/Presentation/WebApi/WebApi.csproj", "Backend/Src/Presentation/WebApi/"]
COPY ["Backend/Src/Infrastructure/Infrastructure/Infrastructure.csproj", "Backend/Src/Infrastructure/Infrastructure/"]
COPY ["Backend/Src/Core/Common/Common.csproj", "Backend/Src/Core/Common/"]
COPY ["Backend/Src/Core/Application/Application.csproj", "Backend/Src/Core/Application/"]
COPY ["Backend/Src/Core/Domain/Domain.csproj", "Backend/Src/Core/Domain/"]
COPY ["Backend/Src/Infrastructure/Persistence/Persistence.csproj", "Backend/Src/Infrastructure/Persistence/"]
RUN dotnet restore "Backend/Src/Presentation/WebApi/WebApi.csproj"
COPY . .
WORKDIR "/src/Backend/Src/Presentation/WebApi"
RUN dotnet build "WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Trip.WebApi.dll"]