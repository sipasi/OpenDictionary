#nullable enable

using System;
using System.Collections.Generic;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.Maui;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

using OpenDictionary.Services.Keys;

namespace OpenDictionary.ViewModels;

[INotifyPropertyChanged]
public partial class AppThemeObservable
{
    private AppTheme current;

    private static AppTheme UserTheme { set => Application.Current!.UserAppTheme = value; }

    public AppTheme Current
    {
        get => current;
        set
        {
            if (SetProperty(ref current, value) is false)
            {
                return;
            }

            Preferences.Set(PreferencesKeys.Theme.UserAppTheme, value.ToString());

            UserTheme = value;
        }
    }

    public AppThemeObservable()
    {
        var value = Preferences.Get(PreferencesKeys.Theme.UserAppTheme, nameof(AppTheme.Dark));

        AppTheme theme = (AppTheme)Enum.Parse(typeof(AppTheme), value);

        Current = theme;
    }

    [RelayCommand]
    private void UpdateRadioButtons(IList<IView> children)
    {
        foreach (var item in children)
        {
            var radioButton = item as RadioButton;

            if (radioButton is null)
            {
                return;
            }

            AppTheme theme = (AppTheme)radioButton.Value;

            if (theme == current)
            {
                radioButton.IsChecked = true;

                UserTheme = theme;

                return;
            }
        }
    }
}