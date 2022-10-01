FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/GitHubActionsSample.Api/.", "GitHubActionsSample.Api/"]
COPY ["./nuget.config", "GitHubActionsSample.Api/"]
RUN dotnet restore "GitHubActionsSample.Api/GitHubActionsSample.Api.csproj"
#COPY . .
#WORKDIR "/GitHubActionsSample.Api"
RUN dotnet build "GitHubActionsSample.Api/GitHubActionsSample.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GitHubActionsSample.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GitHubActionsSample.Api.dll"]
