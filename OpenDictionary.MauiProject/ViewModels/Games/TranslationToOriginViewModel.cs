using Microsoft.Maui.Controls;

using OpenDictionary.Collections.Storages;
using OpenDictionary.Models;

namespace OpenDictionary.ViewModels.Games;

[QueryProperty(nameof(Id), nameof(Id))]
public class TranslationToOriginViewModel : OpenDictionary.Games.WordConformities.ViewModels.TranslationToOriginViewModel
{
    public TranslationToOriginViewModel(IStorage<WordGroup> storage)
        : base(storage) { }

    protected override string GetButtonText(Word word) => word.Origin;

    protected override string GetQuestionText(Word word) => word.Translation;
}