using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramUpdater;
using TelegramUpdater.FilterAttributes.Attributes;
using TelegramUpdater.RainbowUtilities;
using TelegramUpdater.UpdateContainer;
using TelegramUpdater.UpdateHandlers.Scoped.ReadyToUse;

namespace GuessingGameBot.UpdateHandlers.Messages
{
    [Command(command:"guess"), Private]
    internal sealed class Game : MessageHandler
    {
        protected override async Task HandleAsync(IContainer<Message> cntr)
        {
            var mySecretNumber = Random.Shared.Next(1, 1001);
            await ResponseAsync("OK, i've selected a number between 0 and 1000");

            while (true)
            {
                var input = await AwaitTextInputAsync(
                    TimeSpan.FromSeconds(30),
                    "So what's you guess?",
                    replyMarkup: new ReplyKeyboardMarkup(
                        new KeyboardButton("surrender"))
                    { 
                        ResizeKeyboard = true,
                        InputFieldPlaceholder = "What do you guess ?1"
                    },
                    onTimeOut: TimedOut,
                    onUnrelatedUpdate: Unrelated);

                if (input == null)
                    break;

                if (input == "surrender")
                {
                    await ResponseAsync("OK, You losser", replyMarkup: new ReplyKeyboardRemove());
                    return;
                }

                if (int.TryParse(input, out var guessed))
                {
                    if (guessed < mySecretNumber)
                        await ResponseAsync("Try a bigger number.");
                    else if (guessed > mySecretNumber)
                        await ResponseAsync("Try an smaller number.");
                    else
                    {
                        await ResponseAsync("Whooah you got it 🍕");
                        await ResponseAsync("🌭", replyMarkup: new ReplyKeyboardRemove());
                        return;
                    }
                }
                else
                {
                    await ResponseAsync("Please enter a number and try again.");
                }
            }
        }

        private async Task Unrelated(IUpdater arg1, ShiningInfo<long, Update> arg2)
        {
            await ResponseAsync("Please enter a numeric text message.");
        }

        private async Task TimedOut(CancellationToken arg)
        {
            await ResponseAsync(
                "Come back when you got enough time.",
                replyMarkup: new ReplyKeyboardRemove(),
                cancellationToken: arg);
        }
    }
}
