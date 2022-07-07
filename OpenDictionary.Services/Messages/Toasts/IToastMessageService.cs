
using System.Threading.Tasks;

namespace OpenDictionary.Services.Messages.Toasts;

public interface IToastMessageService
{
    Task Show(string message);
}