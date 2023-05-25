using System.Globalization;

using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

using OpenDictionary.Services.Globalization;
using OpenDictionary.Services.Keys;
using OpenDictionary.ViewModels.Settings;

namespace OpenDictionary.Views.Pages;

public partial class SettingsPage : ContentPage
{
    private readonly SettingsViewModel viewModel;

    public SettingsPage(SettingsViewModel viewModel, ICultureInfoService cultureService)
    {
        InitializeComponent();

        BindingContext = this.viewModel = viewModel;

        originPicker.SelectedItem = cultureService.PreferOrigin;
        translationPicker.SelectedItem = cultureService.PreferTranslation;
    }

    private void OriginCultureChanged(object sender, System.EventArgs e)
    {
        string iso = GetTwoLetterIso(sender);

        Preferences.Set(PreferencesKeys.CultureInfo.PreferOrigin, iso);
    }
    private void TranslationCultureChanged(object sender, System.EventArgs e)
    {
        string iso = GetTwoLetterIso(sender);

        Preferences.Set(PreferencesKeys.CultureInfo.PreferTranslation, iso);
    }

    private static string GetTwoLetterIso(object sender)
    {
        var picker = sender as Picker;
        var culture = picker!.SelectedItem as CultureInfo;

        return culture!.TwoLetterISOLanguageName;
    }
}