using OpenDictionary.Collections.Storages;
using OpenDictionary.Models;
using OpenDictionary.Services.Messages.Dialogs;
using OpenDictionary.Services.Navigations;

namespace OpenDictionary.Games.WordConformities.ViewModels;

public class TranslationToOriginViewModel : WordConformityViewModel
{
    public TranslationToOriginViewModel(IStorage<WordGroup> storage, IDialogMessageService dialog, INavigationService navigation)
        : base(storage, dialog, navigation) { }

    protected override string GetButtonText(Word word) => word.Origin;

    protected override string GetQuestionText(Word word) => word.Translation;
}