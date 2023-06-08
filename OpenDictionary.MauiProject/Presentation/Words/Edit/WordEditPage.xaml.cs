using Microsoft.Maui.Controls;

using OpenDictionary.Presentation.Shared;
using OpenDictionary.Words.ViewModels;

namespace OpenDictionary.Words.Pages;

public partial class WordEditPage : AsyncContentPage
{
    public WordEditPage(WordEditViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}