using OpenDictionary.Collections.Storages;
using OpenDictionary.Models;

using Xamarin.Forms;

namespace OpenDictionary.ViewModels.Games
{
    [QueryProperty(nameof(Id), nameof(Id))]
    internal class TranslationToOriginViewModel : OpenDictionary.Games.WordConformities.ViewModels.TranslationToOriginViewModel
    {
        public TranslationToOriginViewModel(IStorage<WordGroup> storage)
            : base(storage) { }
    }
}