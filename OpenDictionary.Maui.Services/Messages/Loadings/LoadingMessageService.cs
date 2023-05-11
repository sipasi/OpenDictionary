using System;
using System.Threading.Tasks;

using CommunityToolkit.Maui.Views;

using Microsoft.Maui.Controls;

using OpenDictionary.Views.Popups;

namespace OpenDictionary.Services.Messages.Loadings;

internal class LoadingMessageService : ILoadingMessageService
{
    public Task Show(string title, string message, Func<Task> task)
    {
        var popup = new PopupLoading(title, message, task);

        return Shell.Current.ShowPopupAsync(popup);
    }
}