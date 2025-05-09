﻿using System.ComponentModel.DataAnnotations;
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
    [FillPropertyRetry(FillingError.ValidationError, 2)]
    public string? LastName { get; set; } // can be null, It's Null-able!

    [Required]
    [Range(13, 120)]
    [FillPropertyRetry(FillingError.ValidationError, 2)]
    public int Age { get; set; }

    public override string ToString()
    {
        return string.Format("{0} {1}, {2} years old.", FirstName, LastName?? "", Age);
    }

    public override async Task OnBeginAsk<TForm>(FormFillingContext<TForm> fillerContext, CancellationToken cancellationToken)
    {
        await fillerContext.SendMessage(
            $"Please send me a value for {fillerContext.PropertyName}",
            replyMarkup: new ForceReplyMarkup(),
            cancellationToken: cancellationToken);
    }

    public override Task OnSuccess<TForm>(FormFillingContext<TForm, OnSuccessContext> fillerContext, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public override async Task OnValidationError<TForm>(FormFillingContext<TForm, ValidationErrorContext> fillerContext, CancellationToken cancellationToken)
    {
        if (fillerContext.Context.RequiredItemNotSupplied)
        {
            await fillerContext.SendMessage($"{fillerContext.PropertyName} was required! You can't just leave it.", cancellationToken: cancellationToken);
        }
        else
        {
            await fillerContext.SendMessage($"You input is invalid for {fillerContext.PropertyName}.\n" +
                string.Join("\n", fillerContext.Context.ValidationResults.Select(
                    x => x.ErrorMessage)), cancellationToken: cancellationToken);
        }
    }
}
