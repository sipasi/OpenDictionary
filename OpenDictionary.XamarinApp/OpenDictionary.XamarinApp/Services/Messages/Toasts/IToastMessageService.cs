
using System.Threading.Tasks;

using Xamarin.Forms;

namespace OpenDictionary.Services.Messages.Toasts
{
    public interface IToastMessageService
    {
        Task Show(string message, Color background, Color foreground);
    }
}