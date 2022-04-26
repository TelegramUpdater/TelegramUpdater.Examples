using Telegram.Bot.Types;
using TelegramUpdater.FilterAttributes.Attributes;
using TelegramUpdater.UpdateContainer;
using TelegramUpdater.UpdateHandlers.Scoped.ReadyToUse;

namespace StateBot.UpdateHandlers.Messages
{
    [Command(command: "fill")]
    public sealed class FillMePlease : MessageHandler
    {
        protected override async Task HandleAsync(IContainer<Message> _)
        {
            await ResponseAsync("Ok, what's your name?");

            Container.SetNumericState("fill", From!, 1);
        }
    }
}
