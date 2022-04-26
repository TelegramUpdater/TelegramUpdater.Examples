using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;
using TelegramUpdater;
using TelegramUpdater.RainbowUtilities;

namespace StateBot
{
    internal sealed class UpdatesComing : AbstractPreUpdateProcessor
    {
        public override Task<bool> PreProcessor(ShiningInfo<long, Update> shiningInfo)
        {
            Updater.Logger.LogInformation(
                "Received update of type {type}", shiningInfo.Value.Type);
            return Task.FromResult(true);
        }
    }
}
