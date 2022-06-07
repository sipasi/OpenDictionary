
using System.Threading.Tasks;

using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

using OpenDictionary.Services.Messages.Toasts;

namespace OpenDictionary.MauiProject.Services.Messages.Toasts;

public class ToastMessageService : IToastMessageService
{
    public Task Show(string message)
    {
        IToast toast = Toast.Make(message);

        return toast.Show();
    }
}