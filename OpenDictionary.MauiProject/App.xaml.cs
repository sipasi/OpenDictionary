using Microsoft.Maui.Controls;

using OpenDictionary.Services.Themes;

namespace OpenDictionary.MauiProject;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        ApplicationTheme.SetLastTheme();

        MainPage = new AppShell();
    }
}