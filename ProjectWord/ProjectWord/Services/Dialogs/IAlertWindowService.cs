#nullable enable
using System.Threading.Tasks;

namespace OpenDictionary.Services.Dialogs
{
    public interface IAlertWindowService
    {
        Task Show(string title, string message, string ok);
    }
}
