using System;

using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

using OpenDictionary.DependencyInjection;
using OpenDictionary.Services.Keys;

namespace OpenDictionary.MauiProject;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        SetTheme();

        DiContainer.Init();

        MainPage = new AppShell();
    }

    private void SetTheme()
    {
        var value = Preferences.Get(PreferencesKeys.Theme.UserAppTheme, nameof(AppTheme.Dark));

        AppTheme theme = (AppTheme)Enum.Parse(typeof(AppTheme), value);

        UserAppTheme = theme;
    }
}