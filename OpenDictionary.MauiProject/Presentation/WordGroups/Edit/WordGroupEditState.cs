using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace OpenDictionary.WordGroups.ViewModels;

public sealed partial class WordGroupEditState : ObservableObject
{
    [ObservableProperty]
    private bool isEditInfo;
    [ObservableProperty]
    private bool isAddWords;

    public void AsEditInfo() => Set(isEditInfo: true);
    public void AsAddWords() => Set(isEditInfo: false);

    [RelayCommand]
    private void ChangeState() => Set(isEditInfo: !IsEditInfo);

    private void Set(bool isEditInfo)
    {
        IsEditInfo = isEditInfo;
        IsAddWords = !isEditInfo;
    }
}
