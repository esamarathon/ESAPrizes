FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS builder
RUN dotnet --version
WORKDIR /build
COPY ESAPrizes.csproj .
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release --no-restore -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-alpine
WORKDIR /app
COPY --from=builder /build/out/ ./
CMD [ "dotnet", "ESAPrizes.dll"]