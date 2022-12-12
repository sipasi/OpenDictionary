using System;
using System.Threading.Tasks;

namespace OpenDictionary.Services.Messages.Toasts;

public static class ToastMessageExtensions
{
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