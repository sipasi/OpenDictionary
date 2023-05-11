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
        PopupDialog dialog = new(title, message, accept, cancel);

        var result = await Shell.Current.ShowPopupAsync(dialog) as PopupDialog.Result;

        if (result is null)
        {
            return DialogResult.Cancel;
        }

        return result.IsOk() ? DialogResult.Ok : DialogResult.Cancel;
    }
}