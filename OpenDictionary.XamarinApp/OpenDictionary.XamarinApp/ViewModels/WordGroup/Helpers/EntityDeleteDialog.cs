using System.Threading.Tasks;

using OpenDictionary.Services.Messages.Dialogs;

namespace OpenDictionary.ViewModels.Helpers
{
    internal static class EntityDeleteDialog
    {
        public static Task<DialogResult> Show(IDialogMessageService dialog)
        {
            string title = "Delete";

            string message = $"You will lose your data permanently";

            string ok = "Yes";

            string cancel = "No";

            return dialog.Show(title, message, ok, cancel);
        }
    }
}