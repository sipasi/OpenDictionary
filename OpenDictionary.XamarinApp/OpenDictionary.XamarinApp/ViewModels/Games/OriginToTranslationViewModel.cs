

using OpenDictionary.Collections.Storages;
using OpenDictionary.Models;

namespace OpenDictionary.ViewModels.Games
{
    public class OriginToTranslationViewModel : WordConformityViewModel
    {
        public OriginToTranslationViewModel(IStorage<WordGroup> storage)
            : base(storage) { }

        protected override string GetButtonText(Word word) => word.Translation;

        protected override string GetQuestionText(Word word) => word.Origin;
    }
}