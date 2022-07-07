using CommunityToolkit.Mvvm.ComponentModel;


namespace OpenDictionary.Observables.Metadatas;

[INotifyPropertyChanged]
public sealed partial class DefinitionObservable
{
    [ObservableProperty]
    private string? value;
    [ObservableProperty]
    private string? example;
}