using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Markup;

using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;

using OpenDictionary.Fonts.Icons;

namespace OpenDictionary.MauiProject;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()

            .UseMauiCommunityToolkitMarkup()
            .UseMauiCommunityToolkit()

            .ConfigureFonts(fonts =>
            {
                fonts.AddFont(filename: "OpenSans-Regular.ttf", alias: "OpenSansRegular");
                fonts.AddFont(filename: "OpenSans-Semibold.ttf", alias: "OpenSansSemibold");

                fonts.AddFont(filename: "remixicon.ttf", alias: RemixIconAsset.Alias);
            });

        return builder.Build();
    }
}