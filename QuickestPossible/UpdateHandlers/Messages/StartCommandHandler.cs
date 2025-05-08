using Telegram.Bot.Types.ReplyMarkups;
using TelegramUpdater.FilterAttributes.Attributes;
using TelegramUpdater.UpdateContainer;
using TelegramUpdater.UpdateContainer.UpdateContainers;
using TelegramUpdater.UpdateHandlers.Scoped.ReadyToUse;

namespace QuickestPossible.UpdateHandlers.Messages
{
    [Command(command: "ok")]
    internal sealed class StartCommandHandler : MessageHandler
    {
        protected override async Task HandleAsync(MessageContainer cntr)
        {
            var msg = await Response("Are ya ok?",
                replyMarkup: MarkupExtensions.BuildInlineKeyboards(x =>
                    x.AddItem(InlineKeyboardButton.WithCallbackData("Yes"))
                    .AddItem(InlineKeyboardButton.WithCallbackData("No"))));

            var callback = await cntr.ChannelButtonClick(TimeSpan.FromMinutes(30), new(@"Yes|No"));

            if (callback is not null)
            {
                await callback.Edit(text: $"Why {callback.Update.Data}?");
            }
            else
            {
                await Response("Slow");
            }
        }
    }
}
