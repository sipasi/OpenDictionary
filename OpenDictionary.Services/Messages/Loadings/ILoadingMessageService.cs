using System;
using System.Threading.Tasks;

namespace OpenDictionary.Services.Messages.Loadings;

public interface ILoadingMessageService
{
    Task Show(string title, string message, Func<Task> task);
}