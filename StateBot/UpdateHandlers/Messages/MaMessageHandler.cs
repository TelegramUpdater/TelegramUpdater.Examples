using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramUpdater;
using TelegramUpdater.FilterAttributes.Attributes;
using TelegramUpdater.UpdateChannels.ReadyToUse;
using TelegramUpdater.UpdateContainer;
using TelegramUpdater.UpdateHandlers.Scoped.ReadyToUse;

namespace StateBot.UpdateHandlers.Messages;

[Command("create", "Create a new quiz"), Private]
internal sealed class MyMessageHandler : MessageHandler
{
    protected override async Task HandleAsync(IContainer<Message> cntr)
    {
        await ResponseAsync("Ok answer my question within 10s.");
        var poll = await BotClient.SendPollAsync(
            Chat.Id, "How are you ?", new string[]
            {
                "Ahh fine thanks",
                "Fuck the world"
            },
            isAnonymous: false,
            allowsMultipleAnswers: false,
            type: Telegram.Bot.Types.Enums.PollType.Quiz, correctOptionId: 1,
            closeDate: DateTime.UtcNow.AddSeconds(10));

        var answer = await OpenChannelAsync(new PollAnswerChannel(
            TimeSpan.FromSeconds(10), new Filter<PollAnswer>(
                (_, x) => x.User.Id == ActualUpdate.From!.Id && x.PollId == poll.Poll!.Id)));

        if (answer is not null)
        {
            if (answer.Update.OptionIds[0] == 0)
            {
                await ResponseAsync($"You're wrong");
            }
            else
            {
                await ResponseAsync($"That's right.");
            }
        }
    }
}
