using Microsoft.Maui.Controls;

using OpenDictionary.Databases;
using OpenDictionary.Services.Messages.Dialogs;
using OpenDictionary.Services.Navigations;

namespace OpenDictionary.ViewModels.Games;

[QueryProperty(nameof(Id), nameof(Id))]
public class TranslationToOriginViewModel : OpenDictionary.Games.WordConformities.ViewModels.TranslationToOriginViewModel
{
    public TranslationToOriginViewModel(IDatabaseConnection<AppDatabaseContext> connection, IDialogMessageService dialog, INavigationService navigation)
        : base(connection, dialog, navigation) { }
}