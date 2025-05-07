// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramUpdater;
using TelegramUpdater.ExceptionHandlers;
using TelegramUpdater.UpdateContainer;
using UpdaterProduction;


var updater = new UpdaterBuilder(
    "BOT_TOKEN")
    .StepOne(
        maxDegreeOfParallelism: 10,   // maximum update process tasks count at the same time
                                      // Eg: first 10 updates are answers quickly, but others should wait
                                      // for any of that 10 to be done.

        allowedUpdates: [UpdateType.Message, UpdateType.CallbackQuery])

    // TODO:
    //.StepTwo(inherit: false)

    .StepTwo(CommonExceptions.ParsingException(
        (updater, ex) =>
        {
            updater.Logger.LogWarning(exception: ex, "Handler has entity parsing error!");
            return Task.CompletedTask;
        },
        allowedHandlers:
        [
            typeof(AboutMessageHandler),
            typeof(MyScopedMessageHandler)
        ]))

    .StepThree(
        async container => await container.Response("Started!"),
        ReadyFilters.OnCommand("start"))

    .AddScopedUpdateHandler<MyScopedMessageHandler>(UpdateType.Message)

    .AddScopedUpdateHandler<Message>(typeof(AboutMessageHandler), UpdateType.Message); // Other way


// ---------- Start! ----------

var me = await updater.GetMe();
updater.Logger.LogInformation("Start listening to {username}", me.Username);

await updater.Start(); // 🔥 Fire up and block!