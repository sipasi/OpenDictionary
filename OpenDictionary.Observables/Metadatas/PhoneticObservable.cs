using CommunityToolkit.Mvvm.ComponentModel;

namespace OpenDictionary.Observables.Metadatas;

[INotifyPropertyChanged]
public sealed partial class PhoneticObservable
{
    [ObservableProperty]
    private string? value = string.Empty;
    [ObservableProperty]
    private string? audio = string.Empty;
}