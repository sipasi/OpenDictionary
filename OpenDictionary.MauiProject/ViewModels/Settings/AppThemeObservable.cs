#nullable enable

using System.Collections.Generic;

using CommunityToolkit.Mvvm.ComponentModel;

using OpenDictionary.Services.Themes;
using OpenDictionary.Styles.Themes;

namespace OpenDictionary.ViewModels.Settings;

public partial class AppThemeObservable : ObservableObject
{
    private Theme current;

    public IReadOnlyList<Theme> Themes { get; }

    public Theme Current
    {
        get => current;
        set
        {
            if (SetProperty(ref current, value) is false)
            {
                return;
            }

            ApplicationTheme.Current = value;
        }
    }

    public AppThemeObservable()
    {
        current = ApplicationTheme.Current;

        Themes = ThemeContainer.Values;
    }
}