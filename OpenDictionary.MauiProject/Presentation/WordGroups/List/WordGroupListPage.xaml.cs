using OpenDictionary.Presentation.Shared;
using OpenDictionary.WordGroups.ViewModels;

namespace OpenDictionary.Views.Pages;

public partial class WordGroupListPage : AsyncContentPage
{
    public WordGroupListPage(WordGroupInfoList viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}