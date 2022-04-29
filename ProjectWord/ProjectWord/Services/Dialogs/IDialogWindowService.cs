#nullable enable
using System.Threading.Tasks;

namespace OpenDictionary.Services.Dialogs
{
    public interface IDialogWindowService
    {
        Task<DialogResult> Show(string title, string message, string ok, string cancel);
    }
}
