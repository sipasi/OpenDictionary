
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.Input;

using OpenDictionary.ExternalAppTranslation;

namespace OpenDictionary.WordGroups.ViewModels;

public sealed class WordGroupTranslateCommands
{
    private readonly WordGroupEditViewModel viewModel;
    private readonly IExternalTranslator translator;

    public AsyncRelayCommand OriginTranslateCommand { get; }

    public WordGroupTranslateCommands(WordGroupEditViewModel viewModel, IExternalTranslator translator)
    {
        this.viewModel = viewModel;
        this.translator = translator;

        OriginTranslateCommand = new(OriginTranslate);
    }

    private async Task OriginTranslate()
    {
        viewModel.OriginCulture ??= "en";
        viewModel.TranslationCulture ??= "uk";

        if (viewModel.Origin is null || viewModel.OriginCulture is null || viewModel.TranslationCulture is null)
        {
            return;
        }

        await translator.Open(viewModel.Origin, new()
        {
            From = new(viewModel.OriginCulture),
            To = new(viewModel.TranslationCulture)
        });
    }
}