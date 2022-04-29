using System.Threading.Tasks;

using OpenDictionary.Services.Dialogs;

namespace OpenDictionary.ViewModels.Helpers
{
    internal static class EntityDeleteDialog
    {
        public static Task<DialogResult> Show(IDialogWindowService dialog)
        {
            string title = "Delete";

            string message = $"You will lose your data permanently";

            string ok = "Yes";

            string cancel = "No";

            return dialog.Show(title, message, ok, cancel);
        }
    }
}