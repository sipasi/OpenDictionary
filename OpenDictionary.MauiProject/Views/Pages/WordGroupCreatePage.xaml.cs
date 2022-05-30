using Microsoft.Maui.Controls;

using OpenDictionary.DependencyInjection;
using OpenDictionary.ViewModels;

namespace OpenDictionary.Views.Pages;

public partial class WordGroupCreatePage : ContentPage
{
    public WordGroupCreatePage()
    {
        InitializeComponent();

        BindingContext = DiContainer.Get<WordGroupEditViewModel>();
    }
}