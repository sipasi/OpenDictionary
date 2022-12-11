using CommunityToolkit.Maui;

using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;

using OpenDictionary.Databases;
using OpenDictionary.DataTransfer;
using OpenDictionary.DataTransfer.ViewModels;
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

        builder.Services.ConfigureOpenDictionary();

        var app = builder.Build();

        DatabaseChecker.Check(app.Services);

        return app;
    }
}