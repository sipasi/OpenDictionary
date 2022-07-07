using System.Threading.Tasks;

namespace OpenDictionary.Services.Navigations;

public interface INavigationService
{
    Task GoBackAsync();
    Task GoToAsync(string route, string parameter, string value);
    Task GoToAsync(string route);
}