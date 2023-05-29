using CommunityToolkit.Mvvm.ComponentModel;


namespace OpenDictionary.Observables.Metadatas;

public sealed partial class DefinitionObservable : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
{
    [ObservableProperty]
    private string? value;
    [ObservableProperty]
    private string? example;
}