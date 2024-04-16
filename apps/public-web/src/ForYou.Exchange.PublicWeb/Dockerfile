FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV ASPNETCORE_URLS=http://+:80;https://+:443

FROM foryou/exchange-base:1.0 AS build
WORKDIR /src

COPY . .
WORKDIR "/src/apps/public-web/src/ForYou.Exchange.PublicWeb"
RUN dotnet publish -c Release -o /app 

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ForYou.Exchange.PublicWeb.dll"]