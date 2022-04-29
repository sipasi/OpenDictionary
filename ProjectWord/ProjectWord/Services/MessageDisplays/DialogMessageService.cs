#nullable enable
using System.Threading.Tasks;

using Xamarin.Forms;

namespace OpenDictionary.Services.MessageDisplays
{
    internal class DialogMessageService : IDialogMessageService
    {
        public async Task<DialogResult> Show(string title, string message, string accept, string cancel)
        {
            var userAnswer = await Shell.Current.DisplayAlert(title, message, accept, cancel);

            var result = userAnswer switch
            {
                true => DialogResult.Ok,
                _ => DialogResult.Cancel
            };

            return result;
        }
    }
}
