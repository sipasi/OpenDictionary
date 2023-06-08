using OpenDictionary.Presentation.Shared;
using OpenDictionary.Words.ViewModels;

namespace OpenDictionary.Words.Pages;

public partial class WordDetailPage : AsyncContentPage
{
    public WordDetailPage(WordDetailViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}