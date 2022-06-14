using Microsoft.Maui.Controls;

using OpenDictionary.Collections.Storages;
using OpenDictionary.Models;

namespace OpenDictionary.ViewModels.Games;

[QueryProperty(nameof(Id), nameof(Id))]
public class OriginToTranslationViewModel : OpenDictionary.Games.WordConformities.ViewModels.OriginToTranslationViewModel
{
    public OriginToTranslationViewModel(IStorage<WordGroup> storage)
        : base(storage) { }
}