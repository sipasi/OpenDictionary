using System.Globalization;

using Microsoft.Maui.Controls;

using OpenDictionary.ViewModels.WordGroups;

namespace OpenDictionary.Views.Pages;

public partial class WordGroupCreatePage : ContentPage
{
    private readonly WordGroupEditViewModel viewModel;

    public WordGroupCreatePage(WordGroupEditViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = this.viewModel = viewModel;
    }

    private void OriginCultureChanged(object sender, System.EventArgs e)
    {
        viewModel.OriginCulture = GetTwoLetterIso(sender);
    }
    private void TranslationCultureChanged(object sender, System.EventArgs e)
    {
        viewModel.TranslationCulture = GetTwoLetterIso(sender);
    }

    private static string GetTwoLetterIso(object sender)
    {
        var picker = sender as Picker;
        var culture = picker!.SelectedItem as CultureInfo;

        return culture!.TwoLetterISOLanguageName;
    }
}