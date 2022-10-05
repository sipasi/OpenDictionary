using CommunityToolkit.Mvvm.ComponentModel;

namespace OpenDictionary.Games.WordConformities.Observables;

[INotifyPropertyChanged]
public sealed partial class AnswerButtonObservable
{
    [ObservableProperty]
    private string text = string.Empty;

    public bool IsCorrect { get; set; }
}