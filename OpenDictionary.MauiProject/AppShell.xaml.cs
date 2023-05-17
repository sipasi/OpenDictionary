using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;

using OpenDictionary.Services.Navigations.Routes;
using OpenDictionary.Services.Themes;
using OpenDictionary.Views.Pages;
using OpenDictionary.Views.Pages.Settings;

namespace OpenDictionary.MauiProject;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(AppRoutes.WordGroup.Create, typeof(WordGroupCreatePage));
        Routing.RegisterRoute(AppRoutes.WordGroup.Detail, typeof(WordGroupDetailPage));
        Routing.RegisterRoute(AppRoutes.Word.Detail, typeof(WordDetailPage));
        Routing.RegisterRoute(AppRoutes.Word.Edit, typeof(WordEditPage));

        Routing.RegisterRoute(AppRoutes.Game.List, typeof(GameListPage));
        Routing.RegisterRoute(AppRoutes.Game.OriginToTranslation, typeof(GameOriginToTranslationPage));
        Routing.RegisterRoute(AppRoutes.Game.TranslationToOrigin, typeof(GameTranslationToOriginPage));

        Routing.RegisterRoute(AppRoutes.Settings.Export, typeof(ExportPage));
        Routing.RegisterRoute(AppRoutes.Settings.Import, typeof(ImportPage));

        Loaded += AppShell_Loaded;
    }

    public void SetListPage() => CurrentItem = list;
    public void SetSettingsPage() => CurrentItem = list;

    private void AppShell_Loaded(object? sender, System.EventArgs e)
    {
        App.Current!.UserAppTheme = AppTheme.Dark;

        ApplicationTheme.SetLastTheme();
    }
}