FROM mcr.microsoft.com/dotnet/aspnet:5.0

EXPOSE 80

WORKDIR /app

COPY src/CurrencyRateBot.Web/bin/Debug/net5.0/publish /app/.

ENTRYPOINT ["dotnet", "CurrencyRateBot.Web.dll"]
