using Microsoft.Maui.Controls;

using OpenDictionary.Collections.Storages;
using OpenDictionary.Models;
using OpenDictionary.Services.Messages.Dialogs;
using OpenDictionary.Services.Navigations;

namespace OpenDictionary.ViewModels.Games;

[QueryProperty(nameof(Id), nameof(Id))]
public class OriginToTranslationViewModel : OpenDictionary.Games.WordConformities.ViewModels.OriginToTranslationViewModel
{
    public OriginToTranslationViewModel(IStorage<WordGroup> storage, IDialogMessageService dialog, INavigationService navigation)
        : base(storage, dialog, navigation) { }
}