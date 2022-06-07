#nullable enable
using System.Threading.Tasks;

using Microsoft.Maui.Controls;

using OpenDictionary.Services.Messages.Alerts;

namespace OpenDictionary.MauiProject.Services.Messages.Alerts;

internal class AlertMessageService : IAlertMessageService
{
    public Task Show(string title, string message, string ok)
    {
        return Shell.Current.DisplayAlert(title, message, ok);
    }
}
