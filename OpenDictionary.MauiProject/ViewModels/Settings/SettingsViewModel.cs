#nullable enable

using System.Threading.Tasks;

using CommunityToolkit.Mvvm.Input;

using OpenDictionary.Databases;
using OpenDictionary.Services.Messages.Dialogs;
using OpenDictionary.Services.Messages.Loadings;
using OpenDictionary.Services.Messages.Toasts;
using OpenDictionary.Services.Navigations;
using OpenDictionary.Services.Navigations.Routes;

namespace OpenDictionary.ViewModels.Settings;

public sealed partial class SettingsViewModel
{
    private readonly INavigationService navigation;

    public AppThemeObservable AppTheme { get; }
    public WordGroupDictionaryViewModel WordGroup { get; }

    public SettingsViewModel(IDatabaseConnection<AppDatabaseContext> connection, INavigationService navigation, IDialogMessageService dialog, IToastMessageService toast, ILoadingMessageService loading)
    {
        this.navigation = navigation;

        AppTheme = new AppThemeObservable();
        WordGroup = new WordGroupDictionaryViewModel(connection, dialog, toast, loading);
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