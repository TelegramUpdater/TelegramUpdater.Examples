using Telegram.Bot.Types;
using TelegramUpdater;
using TelegramUpdater.FillMyForm;
using TelegramUpdater.FillMyForm.CancelTriggers.SealedTriggers;
using TelegramUpdater.UpdateContainer;
using TelegramUpdater.UpdateContainer.UpdateContainers;
using TelegramUpdater.UpdateHandlers.Scoped.ReadyToUse;

namespace QuickExample;

[ApplyFilter(typeof(FormStartFilter))]
internal class FormHandler : MessageHandler
{
    protected override async Task HandleAsync(MessageContainer updateContainer)
    {
        var filler = updateContainer.CreateFormFiller<MySimpleForm>(
            defaultCancelTrigger: new MessageCancelTextTrigger());

        var form = await filler.StartFilling(updateContainer.Sender()!);

        if (form is not null)
        {
            await updateContainer.Response($"Thank you, {form}");
        }
        else
        {
            await updateContainer.Response($"Please try again later.");
        }
    }
}

class FormStartFilter : UpdaterFilter<Message>
{
    public FormStartFilter() : base(ReadyFilters.OnCommand("form")) { }
}
