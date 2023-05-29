using CommunityToolkit.Mvvm.ComponentModel;

namespace OpenDictionary.Observables.Metadatas;

public sealed partial class PhoneticObservable : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
{
    [ObservableProperty]
    private string? word;
    [ObservableProperty]
    private string? pronunciation;
    [ObservableProperty]
    private string? source;

    [ObservableProperty]
    private bool isWebSource;
}