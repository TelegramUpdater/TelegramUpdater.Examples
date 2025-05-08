using Telegram.Bot.Types.ReplyMarkups;
using TelegramUpdater.FilterAttributes.Attributes;
using TelegramUpdater.Filters;
using TelegramUpdater.UpdateContainer;
using TelegramUpdater.UpdateContainer.UpdateContainers;
using TelegramUpdater.UpdateHandlers.Scoped.ReadyToUse;

namespace WorkerService;

[Command(command:"hello"), Private]
public class SimpleMessageHandler : MessageHandler
{
    protected override async Task HandleAsync(MessageContainer container)
    {
        var msg = await Response($"Are you ok? answer quick!",
            replyMarkup: new InlineKeyboardMarkup(
                InlineKeyboardButton.WithCallbackData("Yes i'm OK!", "ok")));

        await container.ChannelButtonClick(
                TimeSpan.FromSeconds(5), new CallbackQueryRegex("ok"))
            .IfNotNull(async answer =>
            {
                await answer.Edit(text: "Well done!");
            })
            .Else(async _ =>
            {
                await msg.Edit("Slow...");
            });
    }
}
