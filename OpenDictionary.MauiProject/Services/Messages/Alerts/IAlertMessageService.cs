#nullable enable
using System.Threading.Tasks;

namespace OpenDictionary.Services.Messages.Alerts;

public interface IAlertMessageService
{
    Task Show(string title, string message, string ok);
}
