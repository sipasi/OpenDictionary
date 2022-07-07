#nullable enable
using System.Threading.Tasks;

namespace OpenDictionary.Services.Messages.Dialogs;

public interface IDialogMessageService
{
    Task<DialogResult> Show(string title, string message, string ok, string cancel);
}
