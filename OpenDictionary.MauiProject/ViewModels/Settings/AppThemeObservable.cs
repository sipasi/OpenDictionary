#nullable enable

using System.Collections.Generic;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.Maui;
using Microsoft.Maui.Controls;
using OpenDictionary.Services.Themes;
using OpenDictionary.Theming;

namespace OpenDictionary.ViewModels.Settings;

[INotifyPropertyChanged]
public partial class AppThemeObservable
{
    private Theme current;

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

            Theme theme = (Theme)radioButton.Value;

            if (theme == current)
            {
                radioButton.IsChecked = true;

                return;
            }
        }
    }
}