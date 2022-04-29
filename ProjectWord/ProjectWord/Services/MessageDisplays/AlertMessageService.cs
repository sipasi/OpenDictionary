#nullable enable
using System.Threading.Tasks;

using Xamarin.Forms;

namespace OpenDictionary.Services.MessageDisplays
{
    internal class AlertMessageService : IAlertMessageService
    {
        public Task Show(string title, string message, string ok)
        {
            return Shell.Current.DisplayAlert(title, message, ok);
        }
    }
}
