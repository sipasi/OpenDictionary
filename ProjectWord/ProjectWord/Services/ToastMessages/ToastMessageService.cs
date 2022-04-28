
using System.Threading.Tasks;

using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Forms;

namespace OpenDictionary.Services.ToastMessages
{
    public class ToastMessageService : IToastMessageService
    {
        public Task Show(string message, Color background, Color foreground)
        {
            MessageOptions messageOptions = new MessageOptions()
            {
                Font = Font.SystemFontOfSize(20),
                Foreground = foreground,
                Message = message,
            };

            ToastOptions options = new ToastOptions
            {
                BackgroundColor = background,
                CornerRadius = new Thickness(10),
                Duration = System.TimeSpan.FromSeconds(2),
                MessageOptions = messageOptions
            };

            return Application.Current.MainPage.DisplayToastAsync(options);
        }
    }
}
