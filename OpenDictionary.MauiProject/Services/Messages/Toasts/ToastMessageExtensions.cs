using System;
using System.Threading.Tasks;

using Microsoft.Maui.Graphics;

using OpenDictionary.Services.Messages.Toasts;

namespace OpenDictionary.XamarinApp.Services.Messages.Toasts;

public static class ToastMessageExtensions
{
    private static readonly Color red = Color.FromArgb("#dc3545");
    private static readonly Color white = Color.FromArgb("#fff");
    private static readonly Color green = Color.FromArgb("#198754");

    public static async Task ShowAfter(this IToastMessageService toast, Func<ValueTask<bool>> operation, string success = "Success", string error = "Error")
    {
        bool result = await operation.Invoke();

        if (result is true)
        {
            await toast.ShowSuccess(success);
        }
        else
        {
            await toast.ShowError(error);
        }
    }

    public static Task ShowSuccess(this IToastMessageService toast, string message = "Success")
    {
        return toast.Show(message);
    }
    public static Task ShowError(this IToastMessageService toast, string message = "Error")
    {
        return toast.Show(message);
    }
}