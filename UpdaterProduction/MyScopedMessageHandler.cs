using Telegram.Bot.Types.ReplyMarkups;
using TelegramUpdater.FilterAttributes.Attributes;
using TelegramUpdater.UpdateContainer;
using TelegramUpdater.UpdateContainer.UpdateContainers;
using TelegramUpdater.UpdateHandlers.Scoped.Attributes;
using TelegramUpdater.UpdateHandlers.Scoped.ReadyToUse;

namespace UpdaterProduction;

[ScopedHandler(Group = -1)]
[Command("test"), Private]
internal class MyScopedMessageHandler : MessageHandler
{
    protected override async Task HandleAsync(MessageContainer container)
    {
        var msg = await Response($"Are you ok? answer quick!",
            replyMarkup: new InlineKeyboardMarkup(
                InlineKeyboardButton.WithCallbackData("Yes i'm OK!", "ok")));

        await container.ChannelButtonClick(TimeSpan.FromSeconds(5), new("ok"))
            .IfNotNull(async answer =>
            {
                await answer.Edit(text: "Well ...");
            })
            .Else(async _ =>
            {
                await container.Response("Slow", sendAsReply: false);
            });
    }
}
