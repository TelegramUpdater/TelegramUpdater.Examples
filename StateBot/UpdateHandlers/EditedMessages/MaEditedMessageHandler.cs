using Telegram.Bot.Types;
using TelegramUpdater.UpdateContainer;
using TelegramUpdater.UpdateHandlers.Scoped.ReadyToUse;

namespace StateBot.UpdateHandlers.EditedMessages
{
    internal sealed class MaEditedMessageHandler : EditedMessageHandler
    {
        protected override Task HandleAsync(IContainer<Message> cntr)
        {
            throw new NotImplementedException();
        }
    }
}
