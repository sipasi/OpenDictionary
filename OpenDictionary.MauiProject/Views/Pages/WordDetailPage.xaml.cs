using Microsoft.Maui.Controls;

using OpenDictionary.DependencyInjection;
using OpenDictionary.ViewModels;

namespace OpenDictionary.Views.Pages;
public partial class WordDetailPage : ContentPage
{
    public WordDetailPage()
    {
        InitializeComponent();

        BindingContext = DiContainer.Get<WordDetailViewModel>();
    }
}