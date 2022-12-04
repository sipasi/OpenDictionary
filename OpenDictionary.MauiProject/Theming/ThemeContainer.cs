using System;

using Microsoft.Maui.Controls;

using OpenDictionary.Resources.Styles.Themes;

namespace OpenDictionary.Theming;

public static class ThemeContainer
{
    public static Theme[] Values { get; } = Enum.GetValues<Theme>();

    public static ResourceDictionary Get(Theme theme) => theme switch
    {
        Theme.Dark => new DarkTheme(),
        Theme.Dracula => new DraculaTheme(),
        Theme.Black => new BlackTheme(),
        Theme.Light => new LightTheme(),
        Theme.Purple => new PurpleTheme(),
        _ => throw new NotImplementedException($"Color theme has not implemented. Name {theme}")
    };
}