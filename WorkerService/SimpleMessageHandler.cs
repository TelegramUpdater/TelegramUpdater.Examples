using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramUpdater.FilterAttributes.Attributes;
using TelegramUpdater.Filters;
using TelegramUpdater.UpdateContainer;
using TelegramUpdater.UpdateHandlers.Scoped.ReadyToUse;

namespace WorkerService;

[Command(command:"hello"), Private]
public class SimpleMessageHandler : MessageHandler
{
    protected override async Task HandleAsync(IContainer<Message> container)
    {
        var msg = await ResponseAsync($"Are you ok? answer quick!",
            replyMarkup: new InlineKeyboardMarkup(
                InlineKeyboardButton.WithCallbackData("Yes i'm OK!", "ok")));

        await container.ChannelButtonClickAsync(
                TimeSpan.FromSeconds(5), new CallbackQueryRegex("ok"))
            .IfNotNull(async answer =>
            {
                await answer.EditAsync(text: "Well ...");
            })
            .Else(async _ =>
            {
                await container.ResponseAsync("Slow", sendAsReply: false);
            });
    }
}
