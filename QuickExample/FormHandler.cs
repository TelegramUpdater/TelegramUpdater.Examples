using Telegram.Bot.Types;
using TelegramUpdater;
using TelegramUpdater.FillMyForm;
using TelegramUpdater.FillMyForm.CancelTriggers.SealedTriggers;
using TelegramUpdater.UpdateContainer;
using TelegramUpdater.UpdateHandlers.Scoped.ReadyToUse;

namespace QuickExample;

[ApplyFilter(typeof(FormStartFilter))]
internal class FormHandler : MessageHandler
{
    protected override async Task HandleAsync(IContainer<Message> updateContainer)
    {
        var filler = new FormFiller<MySimpleForm>(
            updateContainer.Updater,
            defaultCancelTrigger: new MessageCancelTextTrigger());

        var form = await filler.FillAsync(updateContainer.Sender()!);

        if (form is not null)
        {
            await updateContainer.ResponseAsync($"Thank you, {form}");
        }
        else
        {
            await updateContainer.ResponseAsync($"Please try again later.");
        }
    }
}

class FormStartFilter : Filter<Message>
{
    public FormStartFilter() : base(FilterCutify.OnCommand("form")) { }
}
