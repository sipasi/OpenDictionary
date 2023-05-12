
using Microsoft.EntityFrameworkCore;

using OpenDictionary.Databases;
using OpenDictionary.Models;
using OpenDictionary.Services.Messages.Dialogs;
using OpenDictionary.Services.Navigations;

namespace OpenDictionary.Games.WordConformities.ViewModels;

public class TranslationToOriginViewModel : WordConformityViewModel
{
    public TranslationToOriginViewModel(IDatabaseConnection<DbContext> connection, IDialogMessageService dialog, INavigationService navigation)
        : base(connection, dialog, navigation) { }

    protected override string GetButtonText(Word word) => word.Origin;

    protected override string GetQuestionText(Word word) => word.Translation;
}