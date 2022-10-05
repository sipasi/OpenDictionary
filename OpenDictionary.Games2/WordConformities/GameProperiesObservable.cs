using CommunityToolkit.Mvvm.ComponentModel;

namespace OpenDictionary.Games.WordConformities;

[INotifyPropertyChanged]
public sealed partial class GamePropertiesObservable : IProperties
{
    [ObservableProperty]
    private string groupName = string.Empty;

    [ObservableProperty]
    private string question = string.Empty;

    [ObservableProperty]
    private int total;

    [ObservableProperty]
    private int answered;

    [ObservableProperty]
    private int correct;
    [ObservableProperty]
    private int uncorrect;

    public void Clear()
    {
        GroupName = Question = string.Empty;

        Total = Answered = Correct = Uncorrect = 0;
    }
}