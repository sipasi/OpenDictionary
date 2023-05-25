#nullable enable

using System.Threading.Tasks;

using CommunityToolkit.Maui.Views;

using Microsoft.Maui.Controls;

using OpenDictionary.Services.Messages.Dialogs;
using OpenDictionary.Views.Popups;

namespace OpenDictionary.MauiProject.Services.Messages.Dialogs;

internal sealed class PopupDialogMessageService : IDialogMessageService
{
    public async Task<DialogResult> Show(string title, string message, string accept, string cancel)
    {
        DialogPopup dialog = new(title, message, accept, cancel);

        var result = await Shell.Current.ShowPopupAsync(dialog) as DialogPopup.Result;

        return result switch
        {
            DialogPopup.Result.Accept => DialogResult.Accept,

            _ => DialogResult.Cancel,
        };
    }
}