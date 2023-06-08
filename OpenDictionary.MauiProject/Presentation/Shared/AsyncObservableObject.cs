#nullable enable

using System;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;

namespace OpenDictionary.Presentation.Shared;

public class AsyncObservableObject : ObservableObject
{
    public void Initialize()
    {
        OnInitialize().SafeFireAndForget(OnInitializationFailed);
    }

    protected virtual Task OnInitialize() => Task.CompletedTask;

    protected virtual void OnInitializationFailed(string message, string? inner) { }
}
