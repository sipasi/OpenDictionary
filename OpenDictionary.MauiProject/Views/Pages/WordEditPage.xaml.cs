using Microsoft.Maui.Controls;
using Microsoft.Maui.Layouts;

using OpenDictionary.DependencyInjection;
using OpenDictionary.ViewModels;

namespace OpenDictionary.Views.Pages;

public partial class WordEditPage : ContentPage
{
    public WordEditPage()
    {
        InitializeComponent();

        BindingContext = DiContainer.Get<WordEditViewModel>();
    }

    private void AddEntity_Clicked(object sender, System.EventArgs e)
    {
        meaningsLayout.ComputeDesiredSize(0, 0);
    }
}