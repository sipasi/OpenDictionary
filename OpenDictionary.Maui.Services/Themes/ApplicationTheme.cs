#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;

using CommunityToolkit.Maui.Core.Platform;

using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Storage;

using OpenDictionary.Services.Keys;

using OpenDictionary.Styles.Themes;

namespace OpenDictionary.Services.Themes;

public static class ApplicationTheme
{
    public static Theme Current
    {
        get
        {
            string defaultTheme = nameof(Theme.Purple);

            string value = Preferences.Get(PreferencesKeys.Theme.UserAppTheme, defaultTheme) ?? defaultTheme;

            Theme theme = (Theme)Enum.Parse(typeof(Theme), value);

            return theme;
        }
        set
        {
            Preferences.Set(PreferencesKeys.Theme.UserAppTheme, value.ToString());

            Update();
        }
    }

    public static void SetLastTheme() => Update();

    private static void Update()
    {
        ICollection<ResourceDictionary>? merged = Application.Current?.Resources.MergedDictionaries;

        if (merged is null)
        {
            return;
        }

        ResourceDictionary? replace = GetThemeResource();

        if (replace is null)
        {
            return;
        }

        merged.Remove(replace);

        ResourceDictionary theme = ThemeContainer.Get(Current);

#if ANDROID 
        if (OperatingSystem.IsAndroidVersionAtLeast(23))
        {
            if (theme.TryGetValue("Surface", out var value))
            {
                Color? color = value as Color;

                StatusBar.SetColor(color);
            }
        }
#endif

        merged.Add(theme);

        Application.Current!.UserAppTheme = AppTheme.Unspecified;
    }

    private static ResourceDictionary? GetThemeResource()
    {
        ICollection<ResourceDictionary>? merged = Application.Current?.Resources.MergedDictionaries;

        if (merged is null)
        {
            return null;
        }

        return GetGetFromRuntimeCreation(merged) ?? GetFromXamlGenerator(merged);
    }

    private static ResourceDictionary? GetFromXamlGenerator(ICollection<ResourceDictionary> resources)
    {
        ResourceDictionary? resource = resources.FirstOrDefault(static resource => resource.Source?.ToString().Contains("Theme.xaml") ?? false);

        return resource;
    }
    private static ResourceDictionary? GetGetFromRuntimeCreation(ICollection<ResourceDictionary> resources)
    {
        var resource = resources.FirstOrDefault(static resource =>
        {
            var type = resource.GetType();

            var themeType = typeof(ITheme);

            bool result = themeType.IsAssignableFrom(type);

            return result;
        });

        return resource;
    }
}
