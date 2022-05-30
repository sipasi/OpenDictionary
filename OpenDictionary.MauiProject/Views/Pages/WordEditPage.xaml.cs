using Microsoft.Maui.Controls;

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
}