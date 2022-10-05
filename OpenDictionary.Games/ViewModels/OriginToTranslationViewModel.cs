using OpenDictionary.Collections.Storages;
using OpenDictionary.Models;
using OpenDictionary.Services.Messages.Dialogs;
using OpenDictionary.Services.Navigations;

namespace OpenDictionary.Games.WordConformities.ViewModels;

public class OriginToTranslationViewModel : WordConformityViewModel
{
    public OriginToTranslationViewModel(IStorage<WordGroup> storage, IDialogMessageService dialog, INavigationService navigation)
        : base(storage, dialog, navigation) { }

    protected override string GetButtonText(Word word) => word.Translation;

    protected override string GetQuestionText(Word word) => word.Origin;
}