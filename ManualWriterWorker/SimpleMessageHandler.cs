using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramUpdater;
using TelegramUpdater.FilterAttributes.Attributes;
using TelegramUpdater.Helpers;
using TelegramUpdater.UpdateContainer;
using TelegramUpdater.UpdateHandlers.Scoped.ReadyToUse;

namespace ManualWriterWorker;

[Command(command:"ok"), ChatType(ChatTypeFlags.Private)]
public class SimpleMessageHandler : MessageHandler
{
    protected override async Task HandleAsync(IContainer<Message> container)
    {
        var msg = await container.ResponseAsync($"Are you ok? answer quick!",
            replyMarkup: new InlineKeyboardMarkup(
                InlineKeyboardButton.WithCallbackData("Yes i'm OK!", "ok")));

        var message = await AwaitMessageAsync(FilterCutify.Text(), TimeSpan.FromMinutes(5));

        if ( message is not null )
            await message.ResponseAsync("Well done.");
    }
}
