#nullable enable
using System.Threading.Tasks;

using OpenDictionary.Services.Messages.Alerts;

using Xamarin.Forms;

namespace OpenDictionary.XamarinApp.Services.Messages.Alerts
{
    internal class AlertMessageService : IAlertMessageService
    {
        public Task Show(string title, string message, string ok)
        {
            return Shell.Current.DisplayAlert(title, message, ok);
        }
    }
}
