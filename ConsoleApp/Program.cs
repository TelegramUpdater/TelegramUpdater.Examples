﻿// See https://aka.ms/new-console-template for more information
using ConsoleApp;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types.Enums;
using TelegramUpdater;
using TelegramUpdater.ExceptionHandlers;
using TelegramUpdater.UpdateContainer;

var updater = new UpdaterBuilder(
    Credentials.Credentials.BOT_TOKEN)
    .StepOne(
        maxDegreeOfParallelism: 10,   // maximum update process tasks count at the same time
                                      // Eg: first 10 updates are answers quickly, but others should wait
                                      // for any of that 10 to be done.

        allowedUpdates: [UpdateType.Message, UpdateType.CallbackQuery])

    //.StepTwo(inherit: false) // Add default exception handler

    .StepTwo(CommonExceptions.ParsingException( // Add custom exception handler
        (updater, ex) =>
        {
            updater.Logger.LogWarning(exception: ex, "Handler has entity parsing error!");
            return Task.CompletedTask;
        },
        allowedHandlers: [typeof(MyScopedMessageHandler)]
    ))

    .StepThree( // Quick handler
        async container => await container.Response("Started!"),
        ReadyFilters.OnCommand("start"))

    .AddScopedUpdateHandler<MyScopedMessageHandler>(UpdateType.Message); // Scoped handler


// ---------- Start! ----------

var me = await updater.GetMe();
updater.Logger.LogInformation("Start listening to {username}", me.Username);

await updater.Start(); // 🔥 Fire up and block!
