using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace CurrencyRateBot.Bot.Services
{
    public class BotBuiltInHostedService : IHostedService
    {
        private readonly CancellationTokenSource _cts = new();
        private readonly ITelegramBotClient _botClient;

        public BotBuiltInHostedService(IOptions<BotOptions> botOptions)
        {
            var options = botOptions.Value.Key ?? throw new ArgumentNullException("Bot options is not introduced.");
            _botClient = new TelegramBotClient(options);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _botClient.StartReceiving(new DefaultUpdateHandler(HandleUpdateAsync, HandleErrorAsync), _cts.Token);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _cts.Cancel();

            return Task.CompletedTask;
        }


        private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken token)
        {
            return Task.CompletedTask;
        }

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            if (update.Type == UpdateType.Message && update.Message.Type == MessageType.Text)
            {
                var chatId = update.Message.Chat.Id;

                await botClient.SendTextMessageAsync(chatId: chatId, text: $"You said:\n{update.Message.Text}");
            }
        }
    }
}
