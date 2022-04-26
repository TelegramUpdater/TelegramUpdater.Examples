using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramUpdater.FilterAttributes.Attributes;
using TelegramUpdater.UpdateContainer;
using TelegramUpdater.UpdateHandlers.Scoped.ReadyToUse;

namespace StateBot.UpdateHandlers.Messages
{
    [MessageType(MessageType.Text), UserNumericState("fill", 1)]
    public sealed class GettingName : MessageHandler
    {
        protected override async Task HandleAsync(IContainer<Message> cntr)
        {
            var name = ActualUpdate.Text;
            await ResponseAsync($"Ahh then your name is {name}");

            Container.DeleteNumericState("fill", ActualUpdate.From!);
            StopPropagation();
        }
    }
}
