FROM mcr.microsoft.com/dotnet/sdk:5.0 AS sdk
WORKDIR /app

COPY Parser.csproj .
RUN dotnet restore Parser.csproj

COPY . .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS runtime
WORKDIR /app
COPY --from=sdk /app/out ./

ENTRYPOINT ["dotnet", "Parser.dll"]