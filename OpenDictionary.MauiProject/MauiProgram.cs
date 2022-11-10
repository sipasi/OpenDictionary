using CommunityToolkit.Maui;

using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;

using OpenDictionary.Maui.Services;
using OpenDictionary.Services;
using OpenDictionary.Styles.Fonts.Icons;

namespace OpenDictionary.MauiProject;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()

            .UseMauiCommunityToolkit()

            .ConfigureFonts(fonts =>
            {
                fonts.AddFont(filename: "OpenSans-Regular.ttf", alias: "OpenSansRegular");
                fonts.AddFont(filename: "OpenSans-Semibold.ttf", alias: "OpenSansSemibold");

                fonts.AddFont(filename: "remixicon.ttf", alias: RemixIconAsset.Alias);
            });

        builder.Services
            .ConfigureMauiServices()
            .ConfigureAudio()
            .ConfigureDatabase()
            .ConfigureMessagesDialogs()
            .ConfigureIO()
            .ConfigureOnlineDictionary()
            .ConfigureViewModels()
            .ConfigureViews();

        return builder.Build();
    }
}