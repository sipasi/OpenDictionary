using Microsoft.Maui.Controls;

using OpenDictionary.DependencyInjection;

namespace OpenDictionary.MauiProject;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        DiContainer.Init();

        MainPage = new AppShell();
    }
}