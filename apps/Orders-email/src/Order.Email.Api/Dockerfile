FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Order.Email.Api/Order.Email.Api.csproj", "Order.Email.Api/"]
RUN dotnet restore "Order.Email.Api/Order.Email.Api.csproj"
COPY . .
WORKDIR "/src/Order.Email.Api"
RUN dotnet build "Order.Email.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Order.Email.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.Email.Api.dll"]
