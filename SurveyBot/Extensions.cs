using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace SurveyBot
{
    internal static class Extensions
    {
        internal static InlineKeyboardMarkup HowLovelyWeAreButtons()
            => new(
                new InlineKeyboardButton[][]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("SuperLovely", "HLWA_4"),
                        InlineKeyboardButton.WithCallbackData("NotBadLovely", "HLWA_3"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("NotLovely", "HLWA_2"),
                        InlineKeyboardButton.WithCallbackData("IHateYou", "HLWA_1"),
                    },
                    new[] { InlineKeyboardButton.WithCallbackData("Cancel", "cancel") }
                });

        internal static InlineKeyboardMarkup HowFoundFromWhereButtons()
            => new(
                new InlineKeyboardButton[][]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Google", "FFW_4"),
                        InlineKeyboardButton.WithCallbackData("Friends", "FFW_3"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Company", "FFW_2"),
                        InlineKeyboardButton.WithCallbackData("None", "FFW_1"),
                    },
                    new[] { InlineKeyboardButton.WithCallbackData("Cancel", "cancel") }
                });

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
