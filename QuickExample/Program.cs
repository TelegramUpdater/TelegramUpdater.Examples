// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Logging;
using QuickExample;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramUpdater;
using TelegramUpdater.ExceptionHandlers;

await new Updater(new TelegramBotClient("BOT_TOKEN"))
    .AddExceptionHandler(new ExceptionHandler<Exception>(HandleException, inherit: true))
    .AddScopedUpdateHandler<FormHandler>(Telegram.Bot.Types.Enums.UpdateType.Message)
    .Start();

static Task HandleException(IUpdater updater, Exception exception)
{
    updater.Logger.LogError(exception: exception, "Error in handlers");
    return Task.CompletedTask;
}