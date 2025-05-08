using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using TelegramUpdater;

namespace ManualWriterWorker
{
    /// <summary>
    /// This manual wrtiter uses <see href="https://github.com/TelegramBots/Telegram.Bot.Extensions.Polling"/>
    /// Package to receive updates and write them to updater.
    /// </summary>
    public class MyManualWriter : AbstractUpdateWriter
    {
        public MyManualWriter(IUpdater updater) : base(updater)
        {
        }

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            await EnqueueUpdate(update, cancellationToken);
        }

        Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is ApiRequestException apiRequestException)
            {
                Updater.Logger.LogError(exception: apiRequestException, message: "Error in MyManualWriter");
            }
            return Task.CompletedTask;
        }

        protected override async Task Execute(CancellationToken stoppingToken)
        {
            Logger.LogInformation("Getting updates from my manual writer using Polling extesion.");

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { } // receive all update types
            };

            try
            {
                await Updater.BotClient.ReceiveAsync(
                    HandleUpdateAsync,
                    HandleErrorAsync,
                    receiverOptions,
                    stoppingToken
                );
            }
            catch (OperationCanceledException)
            {
            }
        }
    }
}