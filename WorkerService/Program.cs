using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramUpdater;
using TelegramUpdater.Hosting;
using WorkerService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((builder, services) =>
    {
        var botToken = builder.Configuration.GetSection("TelegramUpdater:BotToken")
            .Get<string>() ?? throw new InvalidOperationException("Bot token not found.");

        services.AddTelegramUpdater(
            botToken,
            new UpdaterOptions(
                maxDegreeOfParallelism: 10, // maximum update process tasks count at the same time
                                            // Eg: first 10 updates are answers quickly, but others should wait
                                            // for any of that 10 to be done.
                allowedUpdates: [UpdateType.Message, UpdateType.CallbackQuery]),

            (builder) => builder
                // This commented piece of code won't work as it doesn't register the handler type into DI.
                //.Execute(updater =>
                //    updater.AddScopedUpdateHandler<SimpleMessageHandler>(UpdateType.Message))
                .AddScopedUpdateHandler<Message>(typeof(SimpleMessageHandler), UpdateType.Message)
                .AddDefaultExceptionHandler());
    })
    .Build();

await host.RunAsync();
