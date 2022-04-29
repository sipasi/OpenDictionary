#nullable enable
using System.Threading.Tasks;

namespace OpenDictionary.Services.MessageDisplays
{
    public interface IAlertMessageService
    {
        Task Show(string title, string message, string ok);
    }
}
