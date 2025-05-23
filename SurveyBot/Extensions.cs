﻿using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace SurveyBot
{
    internal static class Extensions
    {
        internal static InlineKeyboardMarkup HowLovelyWeAreButtons()
            => new(
                [
                    [
                        InlineKeyboardButton.WithCallbackData("Super Lovely", "HLWA_4"),
                        InlineKeyboardButton.WithCallbackData("Not Bad Lovely", "HLWA_3"),
                    ],
                    [
                        InlineKeyboardButton.WithCallbackData("Not Lovely", "HLWA_2"),
                        InlineKeyboardButton.WithCallbackData("I Hate You", "HLWA_1"),
                    ],
                    [InlineKeyboardButton.WithCallbackData("Cancel", "cancel")]
                ]);

        internal static InlineKeyboardMarkup HowFoundFromWhereButtons()
            => new(
                [
                    [
                        InlineKeyboardButton.WithCallbackData("Google", "FFW_4"),
                        InlineKeyboardButton.WithCallbackData("Friends", "FFW_3"),
                    ],
                    [
                        InlineKeyboardButton.WithCallbackData("Company", "FFW_2"),
                        InlineKeyboardButton.WithCallbackData("None", "FFW_1"),
                    ],
                    [InlineKeyboardButton.WithCallbackData("Cancel", "cancel")]
                ]);

        internal static HowLovelyWeAre ToHowLovelyWeAre(this CallbackQuery callbackQuery)
            => callbackQuery switch
            {
                { Data: { } data } when data.StartsWith("HLWA_")
                    => (HowLovelyWeAre)int.Parse(data[5..]),
                _ => throw new InvalidDataException()
            };

        internal static FoundFromWhere ToFoundFromWhere(this CallbackQuery callbackQuery)
            => callbackQuery switch
            {
                { Data: { } data } when data.StartsWith("FFW_")
                    => (FoundFromWhere)int.Parse(data[4..]),
                _ => throw new InvalidDataException()
            };
    }
}
