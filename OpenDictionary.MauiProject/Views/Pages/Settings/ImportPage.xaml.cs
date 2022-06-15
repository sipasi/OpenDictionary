using Microsoft.Maui.Controls;

using OpenDictionary.DependencyInjection;
using OpenDictionary.ViewModels;

namespace OpenDictionary.Views.Pages.Settings;

public partial class ImportPage : ContentPage
{
    public ImportPage()
    {
        InitializeComponent();

        BindingContext = DiContainer.Get<ImportViewModel>();
    }
}