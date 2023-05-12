using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Controls;

using OpenDictionary.Databases;
using OpenDictionary.Models;
using OpenDictionary.Services.Messages.Dialogs;
using OpenDictionary.Services.Navigations;

namespace OpenDictionary.ViewModels.Games;

[QueryProperty(nameof(Id), nameof(Id))]
public class OriginToTranslationViewModel : OpenDictionary.Games.WordConformities.ViewModels.OriginToTranslationViewModel
{
    public OriginToTranslationViewModel(IDatabaseConnection<AppDatabaseContext> connection, IDialogMessageService dialog, INavigationService navigation)
        : base(connection, dialog, navigation) { }
}