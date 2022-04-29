#nullable enable
using System.Threading.Tasks;

namespace OpenDictionary.Services.MessageDisplays
{
    public interface IDialogMessageService
    {
        Task<DialogResult> Show(string title, string message, string ok, string cancel);
    }
}
