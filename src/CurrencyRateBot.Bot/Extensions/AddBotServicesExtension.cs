using CurrencyRateBot.Bot;
using CurrencyRateBot.Bot.Services;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AddBotServicesExtension
    {
        public static IServiceCollection AddBot(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddHostedService<BotBuiltInHostedService>();
            serviceCollection.AddOptions<BotOptions>().Bind(configuration.GetSection("Bot"));

            return serviceCollection;
        }
    }
}
