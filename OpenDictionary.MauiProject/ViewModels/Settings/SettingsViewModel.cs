#nullable enable

using System.Threading.Tasks;

using CommunityToolkit.Mvvm.Input;

using OpenDictionary.Collections.Storages;
using OpenDictionary.Models;
using OpenDictionary.Services.Messages.Dialogs;
using OpenDictionary.Services.Messages.Toasts;
using OpenDictionary.Services.Navigations;
using OpenDictionary.Services.Navigations.Routes;

namespace OpenDictionary.ViewModels;

public sealed partial class SettingsViewModel
{
    private readonly INavigationService navigation;

    public AppThemeObservable AppTheme { get; }
    public WordGroupDictionaryViewModel WordGroup { get; }

    public SettingsViewModel(IStorage<WordGroup> storage, INavigationService navigation, IDialogMessageService dialog, IToastMessageService toast)
    {
        this.navigation = navigation;

        AppTheme = new AppThemeObservable();
        WordGroup = new WordGroupDictionaryViewModel(storage, dialog, toast);
    }

    [RelayCommand]
    private Task RedirectToImport()
    {
        return navigation.GoToAsync(AppRoutes.Settings.Import);
    }
    [RelayCommand]
    private Task RedirectToExport()
    {
        return navigation.GoToAsync(AppRoutes.Settings.Export);
    }
}