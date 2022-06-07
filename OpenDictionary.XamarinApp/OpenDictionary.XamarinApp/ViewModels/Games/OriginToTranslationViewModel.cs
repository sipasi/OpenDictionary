using OpenDictionary.Collections.Storages;
using OpenDictionary.Models;

using Xamarin.Forms;

namespace OpenDictionary.ViewModels.Games
{
    [QueryProperty(nameof(Id), nameof(Id))]
    internal class OriginToTranslationViewModel : OpenDictionary.Games.WordConformities.ViewModels.OriginToTranslationViewModel
    {
        public OriginToTranslationViewModel(IStorage<WordGroup> storage)
            : base(storage) { }
    }
}