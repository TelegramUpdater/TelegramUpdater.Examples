// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Logging;
using SurveyBot;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.Passport;
using TelegramUpdater;
using TelegramUpdater.FillMyForm;
using TelegramUpdater.FillMyForm.CancelTriggers.SealedTriggers;
using TelegramUpdater.FillMyForm.UpdateCrackers.Crackers;
using TelegramUpdater.Filters;
using TelegramUpdater.UpdateChannels.ReadyToUse;
using TelegramUpdater.UpdateContainer;


await new Updater(new TelegramBotClient(Credentials.Credentials.BOT_TOKEN))
    .AddExceptionHandler<Exception>(HandleException, inherit: true) // Catch all exceptions in handlers
    .AddSingletonUpdateHandler(
        UpdateType.Message, HandleUpdate, ReadyFilters.OnCommand("survey")) // handle command /survey
    .Start(); // Start.

// Callback function to handle /survey command.
async Task HandleUpdate(IContainer<Message> ctnr)
{
    var callbackCancelTrigger = new CallbackQueryCancelTrigger(
        new MyCallbackQueryRegexFilter("^cancel$")); // Cancellation will trigger on "cancel" callback data.

    var filler = new FormFiller<SimpleSurvey>(
        ctnr.Updater,
        ctx => ctx
        // Add custom cracker for each property
        .AddCracker(
            x => x.HowLovelyWeAre,                          // 1. Select the targeted property.
            new CallbackQueryCracker<HowLovelyWeAre>(       // 2. Create a cracker
                new CallbackQueryChannel(                   //      2-1. Use channels to get matching update
                    TimeSpan.FromSeconds(30),               //          2-1-1. A time out for waiting time
                    ReadyFilters.DataMatches(@"^HLWA_")),   //          2-1-2. A filter to match callback data with given regex
                x => x.ToHowLovelyWeAre(),                  //      2-2. Extract a value for the propery from update.
                callbackCancelTrigger)                      //      2-3. Add a cancel trigger.
            )

        .AddCracker(
            x => x.FoundFromWhere,
            new CallbackQueryCracker<FoundFromWhere>(       // Same.
                new CallbackQueryChannel(
                    TimeSpan.FromSeconds(30),
                    ReadyFilters.DataMatches(@"^FFW_")),
                x => x.ToFoundFromWhere(),
                callbackCancelTrigger)));


    var form = await filler.FillAsync(ctnr.Sender()!); // I'm sure the sender is not null, are you?

    if (form is not null) // Form got filled.
    {
        await ctnr.Response($"Thank you, {form}");
    }
    else // Something is wrong
    {
        await ctnr.Response($"Please try again later.");
    }
}

// Handle exceptions.
static Task HandleException(IUpdater updater, Exception exception)
{
    updater.Logger.LogError(exception: exception, "Error in handlers");
    return Task.CompletedTask;
}

class MyCallbackQueryRegexFilter(string pattern): Filter<CallbackQuery>()
{
    public override bool TheyShellPass(CallbackQuery input)
    {
        if (input.Data is null) return false;

        return new StringRegex(pattern).TheyShellPass(input.Data);
    }
}