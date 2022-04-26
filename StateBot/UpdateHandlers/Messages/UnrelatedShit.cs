using Telegram.Bot.Types;
using TelegramUpdater.FilterAttributes.Attributes;
using TelegramUpdater.UpdateContainer;
using TelegramUpdater.UpdateHandlers.Scoped.ReadyToUse;

namespace StateBot.UpdateHandlers.Messages
{
    [UserNumericState("fill", anyState: true)]
    public sealed class UnrelatedShit : MessageHandler
    {
        public UnrelatedShit(): base(1) { }

        protected override async Task HandleAsync(IContainer<Message> cntr)
        {
            await ResponseAsync($"Send me some text.");
        }
    }
}
