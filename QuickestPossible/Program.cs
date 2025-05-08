using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using TelegramUpdater;

var updater = new Updater(
    new TelegramBotClient(Credentials.Credentials.BOT_TOKEN),
    new UpdaterOptions(allowedUpdates: [UpdateType.Message, UpdateType.CallbackQuery]))

    .AddDefaultExceptionHandler()
    .AutoCollectScopedHandlers()
    .CollectSingletonUpdateHandlerCallbacks();

await updater.Start();