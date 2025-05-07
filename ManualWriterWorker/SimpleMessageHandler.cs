using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramUpdater;
using TelegramUpdater.FilterAttributes.Attributes;
using TelegramUpdater.Helpers;
using TelegramUpdater.UpdateContainer;
using TelegramUpdater.UpdateContainer.UpdateContainers;
using TelegramUpdater.UpdateHandlers.Scoped.ReadyToUse;

namespace ManualWriterWorker;

[Command(command:"ok"), ChatType(ChatTypeFlags.Private)]
public class SimpleMessageHandler : MessageHandler
{
    protected override async Task HandleAsync(MessageContainer container)
    {
        var msg = await container.Response($"Are you ok? answer quick!",
            replyMarkup: new InlineKeyboardMarkup(
                InlineKeyboardButton.WithCallbackData("Yes i'm OK!", "ok")));

        var message = await ChannelMessage(ReadyFilters.Text(), TimeSpan.FromMinutes(5));

        if ( message is not null )
            await message.Response("Well done.");
    }
}
