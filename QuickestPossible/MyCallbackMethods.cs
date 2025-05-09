﻿using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramUpdater.FilterAttributes.Attributes;
using TelegramUpdater.UpdateContainer;
using TelegramUpdater.UpdateHandlers.Singleton.Attributes;

namespace QuickestPossible;

internal static class MyCallbackMethods
{
    [Command(command:"start"), Private]
    [SingletonHandlerCallback(UpdateType.Message)]
    public static async Task MyHandlerCallback(IContainer<Message> container)
    {
        await container.Response("Started");
    }
}
