using Microsoft.Maui.Controls;

using OpenDictionary.Databases;
using OpenDictionary.Services.Messages.Dialogs;
using OpenDictionary.Services.Navigations;

namespace OpenDictionary.Games.WordConformities.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public class OriginToTranslation : OriginToTranslationViewModel
{
    public OriginToTranslation(IDatabaseConnection<AppDatabaseContext> connection, IDialogMessageService dialog, INavigationService navigation)
        : base(connection, dialog, navigation) { }
}