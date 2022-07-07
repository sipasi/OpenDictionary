using OpenDictionary.Collections.Storages;
using OpenDictionary.Models;

namespace OpenDictionary.Games.WordConformities.ViewModels;

public class OriginToTranslationViewModel : WordConformityViewModel
{
    public OriginToTranslationViewModel(IStorage<WordGroup> storage)
        : base(storage) { }

    protected override string GetButtonText(Word word) => word.Translation;

    protected override string GetQuestionText(Word word) => word.Origin;
}