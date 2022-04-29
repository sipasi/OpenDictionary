
using System.Threading.Tasks;

using Xamarin.Forms;

namespace OpenDictionary.Services.ToastMessages
{
    public interface IToastMessageService
    {
        Task Show(string message, Color background, Color foreground);
    }
}