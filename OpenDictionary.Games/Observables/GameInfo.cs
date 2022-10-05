using CommunityToolkit.Mvvm.ComponentModel;

namespace OpenDictionary.Games.Observables;

[INotifyPropertyChanged]
public sealed partial class GameInfo
{
    [ObservableProperty]
    private string image = string.Empty;
    [ObservableProperty]
    private string name = string.Empty;
    [ObservableProperty]
    private string description = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsUnlock))]
    [NotifyPropertyChangedFor(nameof(IsLock))]
    private int wordCount;

    [ObservableProperty]
    private int countToUnlock;

    [ObservableProperty]
    private string route = string.Empty;

    public bool IsUnlock => WordCount >= CountToUnlock;
    public bool IsLock => !IsUnlock;
}