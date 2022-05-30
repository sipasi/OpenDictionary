#nullable enable
using System.Threading.Tasks;

using Microsoft.Maui.Controls;

using OpenDictionary.Services.Messages.Dialogs;

namespace OpenDictionary.XamarinApp.Services.Messages.Dialogs;

internal class DialogMessageService : IDialogMessageService
{
    public async Task<DialogResult> Show(string title, string message, string accept, string cancel)
    {
        var userAnswer = await Shell.Current.DisplayAlert(title, message, accept, cancel);

        var result = userAnswer switch
        {
            true => DialogResult.Ok,
            _ => DialogResult.Cancel
        };

        return result;
    }
}
