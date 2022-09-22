using Microsoft.Maui.Controls;

using OpenDictionary.DependencyInjection;
using OpenDictionary.Services.Themes;

namespace OpenDictionary.MauiProject;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        ApplicationTheme.SetLastTheme();

        DiContainer.Init();

        MainPage = new AppShell();
    }
}