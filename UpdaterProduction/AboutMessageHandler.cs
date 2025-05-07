using Telegram.Bot.Types.Enums;
using TelegramUpdater.FilterAttributes.Attributes;
using TelegramUpdater.UpdateContainer.UpdateContainers;
using TelegramUpdater.UpdateHandlers.Scoped.ReadyToUse;

namespace UpdaterProduction;

[Command("about"), Private]
internal class AboutMessageHandler : MessageHandler
{
    protected override async Task HandleAsync(MessageContainer container)
    {
        await Response($"*How about you?", parseMode: ParseMode.Markdown);
    }
}
