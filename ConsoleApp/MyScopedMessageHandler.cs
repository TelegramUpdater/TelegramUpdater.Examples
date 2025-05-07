using TelegramUpdater.FilterAttributes.Attributes;
using TelegramUpdater.Helpers;
using TelegramUpdater.UpdateContainer.UpdateContainers;
using TelegramUpdater.UpdateHandlers.Scoped.ReadyToUse;

namespace ConsoleApp;

[Command(command: "test"), ChatType(ChatTypeFlags.Private)]
internal class MyScopedMessageHandler : MessageHandler
{
    public MyScopedMessageHandler()
    { }

    protected override async Task HandleAsync(MessageContainer container)
    {
        await Response("Tested!");
    }
}
