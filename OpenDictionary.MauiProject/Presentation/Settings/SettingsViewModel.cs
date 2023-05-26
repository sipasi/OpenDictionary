#nullable enable

using System.Threading.Tasks;

using CommunityToolkit.Mvvm.Input;

using OpenDictionary.Databases;
using OpenDictionary.Services.Globalization;
using OpenDictionary.Services.Messages.Dialogs;
using OpenDictionary.Services.Messages.Loadings;
using OpenDictionary.Services.Messages.Toasts;
using OpenDictionary.Services.Navigations;
using OpenDictionary.Services.Navigations.Routes;
using OpenDictionary.ViewModels.Shared;

namespace OpenDictionary.Settings.ViewModels;

public sealed partial class SettingsViewModel
{
    private readonly INavigationService navigation;

    public AppThemeObservable AppTheme { get; }
    public CultureInfoViewModel CultureInfo { get; }
    public WordGroupDictionaryViewModel WordGroup { get; }

    public SettingsViewModel(
        IDatabaseConnection<AppDatabaseContext> connection, INavigationService navigation,
        IDialogMessageService dialog, IToastMessageService toast, ILoadingMessageService loading,
        ICultureInfoService cultureService)
    {
        this.navigation = navigation;

        AppTheme = new AppThemeObservable();
        CultureInfo = new(cultureService);
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