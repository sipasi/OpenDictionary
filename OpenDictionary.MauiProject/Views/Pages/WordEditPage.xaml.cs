using Microsoft.Maui.Controls;
using Microsoft.Maui.Layouts;


using OpenDictionary.ViewModels;

namespace OpenDictionary.Views.Pages;

public partial class WordEditPage : ContentPage
{
    public WordEditPage(WordEditViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }

    private void AddEntity_Clicked(object sender, System.EventArgs e)
    {
        meaningsLayout.ComputeDesiredSize(0, 0);
    }
}