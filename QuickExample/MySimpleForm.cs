using System.ComponentModel.DataAnnotations;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramUpdater.FillMyForm;

namespace QuickExample;

internal class MySimpleForm : AbstractForm
{
    [Required]
    [MinLength(3)]
    [MaxLength(32)]
    [FillPropertyRetry(FillingError.ValidationError, 2)]
    public string FirstName { get; set; } = null!;

    [MinLength(3)]
    [MaxLength(32)]
    public string? LastName { get; set; } // can be null, It's Nullable!

    [Required]
    [Range(13, 120)]
    public int Age { get; set; }

    public override string ToString()
    {
        return string.Format("{0} {1}, {2} years old.", FirstName, LastName?? "", Age);
    }

    public override async Task OnBeginAskAsync<TForm>(FormFillterContext<TForm> fillterContext, CancellationToken cancellationToken)
    {
        await fillterContext.SendTextMessageAsync(
            $"Please send me a value for {fillterContext.PropertyName}",
            replyMarkup: new ForceReplyMarkup(),
            cancellationToken: cancellationToken);
    }

    public override Task OnSuccessAsync<TForm>(FormFillterContext<TForm> fillterContext, OnSuccessContext onSuccessContext, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public override async Task OnValidationErrorAsync<TForm>(FormFillterContext<TForm> fillterContext, ValidationErrorContext validationErrorContext, CancellationToken cancellationToken)
    {
        if (validationErrorContext.RequiredItemNotSupplied)
        {
            await fillterContext.SendTextMessageAsync(
                $"{fillterContext.PropertyName} was required! You can't just leave it.");
        }
        else
        {
            await fillterContext.SendTextMessageAsync(
                $"You input is invalid for {fillterContext.PropertyName}.\n" +
                string.Join("\n", validationErrorContext.ValidationResults.Select(
                    x => x.ErrorMessage)));
        }
    }
}
