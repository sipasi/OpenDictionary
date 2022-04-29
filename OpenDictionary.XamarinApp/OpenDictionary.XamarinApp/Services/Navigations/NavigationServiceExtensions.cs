using System.Threading.Tasks;

using Xamarin.Forms;

namespace OpenDictionary.Services.Navigations
{
    public static class NavigationServiceExtensions
    {
        public static Task GoToAsync<T>(this INavigationService navigation) where T : Page
        {
            return navigation.GoToAsync(route: typeof(T).Name);
        }
        public static Task GoToAsync<T>(this INavigationService navigation, string parameter, string value) where T : Page
        {
            return navigation.GoToAsync(route: typeof(T).Name, parameter, value);
        }
    }
}