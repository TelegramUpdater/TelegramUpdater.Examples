using StateBot;
using Telegram.Bot.Types.Enums;
using TelegramUpdater;

Console.WriteLine("Hello, World!");

var updater = new Updater(
    "2015323878:AAEng0bPfJuAAOAPKgcuhrSDo-K0Jqh24fA",
    new UpdaterOptions(allowedUpdates: new[] { UpdateType.PollAnswer, UpdateType.Message }),
    typeof(UpdatesComing));

updater.AutoCollectScopedHandlers();
updater.AddUserNumericStateKeeper("fill");

var me = await updater.GetMeAsync();
Console.WriteLine(me.Username);

await updater.StartAsync();