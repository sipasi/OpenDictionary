﻿#nullable enable

using System.Threading.Tasks;

using Microsoft.Maui.Controls;

using OpenDictionary.Services.Messages.Dialogs;

namespace OpenDictionary.Maui.Services.Messages.Dialogs;

internal sealed class NativeDialogMessageService : IDialogMessageService
{
    public async Task<DialogResult> Show(string title, string message, string accept, string cancel)
    {
        var answer = await Shell.Current.DisplayAlert(title, message, accept, cancel);

        var result = answer switch
        {
            true => DialogResult.Accept,
            _ => DialogResult.Cancel
        };

        return result;
    }
}