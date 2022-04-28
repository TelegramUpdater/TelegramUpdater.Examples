using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramUpdater;
using TelegramUpdater.UpdateContainer;

var updater = new Updater("BOT_TOKEN_HERE")
    .AddDefaultExceptionHandler()
    .AutoCollectScopedHandlers()
    .AddSingletonUpdateHandler(
        UpdateType.Message, StartCallback, FilterCutify.OnCommand("start"));

await updater.StartAsync();

static async Task StartCallback(IContainer<Message> container)
{
    await container.ResponseAsync("Started, try /guess to begin.");
}