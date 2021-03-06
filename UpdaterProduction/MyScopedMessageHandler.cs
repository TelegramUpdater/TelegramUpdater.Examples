using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramUpdater.FilterAttributes.Attributes;
using TelegramUpdater.UpdateContainer;
using TelegramUpdater.UpdateHandlers.Scoped.ReadyToUse;

namespace UpdaterProduction;

[Command(command:"test"), Private]
internal class MyScopedMessageHandler : MessageHandler
{
    public MyScopedMessageHandler() : base(group: -1)
    { }

    protected override async Task HandleAsync(IContainer<Message> container)
    {
        var msg = await ResponseAsync($"Are you ok? answer quick!",
            replyMarkup: new InlineKeyboardMarkup(
                InlineKeyboardButton.WithCallbackData("Yes i'm OK!", "ok")));

        await container.ChannelButtonClickAsync(TimeSpan.FromSeconds(5), new("ok"))
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
