using Microsoft.Maui.Controls;

using OpenDictionary.Collections.Storages;
using OpenDictionary.Models;
using OpenDictionary.Services.Messages.Dialogs;
using OpenDictionary.Services.Navigations;

namespace OpenDictionary.ViewModels.Games;

[QueryProperty(nameof(Id), nameof(Id))]
public class TranslationToOriginViewModel : OpenDictionary.Games.WordConformities.ViewModels.TranslationToOriginViewModel
{
    public TranslationToOriginViewModel(IStorage<WordGroup> storage, IDialogMessageService dialog, INavigationService navigation)
        : base(storage, dialog, navigation) { }
}