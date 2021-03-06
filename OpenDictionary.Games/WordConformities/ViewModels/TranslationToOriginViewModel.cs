using OpenDictionary.Collections.Storages;
using OpenDictionary.Models;

namespace OpenDictionary.Games.WordConformities.ViewModels;

public class TranslationToOriginViewModel : WordConformityViewModel
{
    public TranslationToOriginViewModel(IStorage<WordGroup> storage)
        : base(storage) { }

    protected override string GetButtonText(Word word) => word.Origin;

    protected override string GetQuestionText(Word word) => word.Translation;
}