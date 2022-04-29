using System;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace OpenDictionary.Services.ToastMessages.Extensions
{
    public static class ToastMessageExtensions
    {
        private static readonly Color red = Color.FromHex("#dc3545");
        private static readonly Color white = Color.FromHex("#fff");
        private static readonly Color green = Color.FromHex("#198754");

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
            return toast.Show(message, background: green, foreground: white);
        }
        public static Task ShowError(this IToastMessageService toast, string message = "Error")
        {
            return toast.Show(message, background: red, foreground: white);
        }
    }
}