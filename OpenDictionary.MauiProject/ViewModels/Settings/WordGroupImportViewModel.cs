#nullable enable

using OpenDictionary.Databases;
using OpenDictionary.DataTransfer.ViewModels;
using OpenDictionary.Models;
using OpenDictionary.Services.Messages.Toasts;
using OpenDictionary.Services.Navigations;

namespace OpenDictionary.ViewModels;

public sealed class WordGroupImportViewModel : ImportJsonViewModel<WordGroup>
{
    public WordGroupImportViewModel(IDatabaseConnection<AppDatabaseContext> connection, INavigationService navigation, IToastMessageService toast)
        : base(connection, navigation, toast) { }
}