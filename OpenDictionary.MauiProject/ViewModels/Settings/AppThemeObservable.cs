#nullable enable

using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;

namespace OpenDictionary.ViewModels;

public class AppThemeObservable : ViewModel
{
    private AppTheme current;

    private static AppTheme UserTheme { set => Application.Current!.UserAppTheme = value; }

    public AppTheme Current
    {
        get => current;
        set
        {
            SetProperty(ref current, value);

            UserTheme = value;
        }
    }


    public Command<IViewContainer<View>> UpdateRadioButtonsCommand { get; }

    public AppThemeObservable()
    {
        Current = Application.Current!.RequestedTheme;

        UpdateRadioButtonsCommand = new Command<IViewContainer<View>>(UpdateRadioButtonsGroup);
    }

    private void UpdateRadioButtonsGroup(IViewContainer<View> container)
    {
        var children = container.Children;

        foreach (var item in children)
        {
            var radioButton = item as RadioButton;

            if (radioButton is null)
            {
                return;
            }

            AppTheme theme = (AppTheme)radioButton.Value;

            if (theme == Current)
            {
                radioButton.IsChecked = true;

                UserTheme = theme;

                return;
            }
        }
    }
}