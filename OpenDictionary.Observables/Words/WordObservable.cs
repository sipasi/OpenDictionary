using CommunityToolkit.Mvvm.ComponentModel;

namespace OpenDictionary.Observables.Words;

[INotifyPropertyChanged]
public sealed partial class WordObservable
{
    [ObservableProperty]
    private string origin = string.Empty;
    [ObservableProperty]
    private string translation = string.Empty;
}