using CommunityToolkit.Maui;

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

            .UseMauiCommunityToolkit()

            .ConfigureFonts(fonts =>
            {
                fonts.AddFont(filename: "OpenSans-Regular.ttf", alias: "OpenSansRegular");
                fonts.AddFont(filename: "OpenSans-Semibold.ttf", alias: "OpenSansSemibold");

                fonts.AddFont(filename: "Icons/Font Awesome 6 Brands-Regular-400.otf", alias: nameof(FontAwesomeBrands));
                fonts.AddFont(filename: "Icons/Font Awesome 6 Regular-400.otf", alias: nameof(FontAwesomeRegular));
                fonts.AddFont(filename: "Icons/Font Awesome 6 Solid-900.otf", alias: nameof(FontAwesomeSolid));
                fonts.AddFont(filename: "Icons/remixicon.ttf", alias: nameof(RemixIcon));
            });

        return builder.Build();
    }
}