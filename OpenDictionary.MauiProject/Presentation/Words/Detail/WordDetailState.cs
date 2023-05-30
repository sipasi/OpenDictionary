#nullable enable

using CommunityToolkit.Mvvm.ComponentModel;

namespace OpenDictionary.Words.ViewModels;

public sealed partial class WordDetailState : ObservableObject
{
    public static readonly string Loading = nameof(Loading);
    public static readonly string Success = nameof(Success);

    [ObservableProperty]
    private string current = Loading;
    [ObservableProperty]
    private bool canChange = true;

    public void AsLoading()
    {
        Current = Loading;
    }

    public void AsSuccess()
    {
        Current = Success;
    }
}
