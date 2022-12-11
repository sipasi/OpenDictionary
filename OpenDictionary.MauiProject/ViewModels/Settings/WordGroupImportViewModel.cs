#nullable enable

using OpenDictionary.Collections.Storages;
using OpenDictionary.DataTransfer.ViewModels;
using OpenDictionary.Models;
using OpenDictionary.Services.Messages.Toasts;
using OpenDictionary.Services.Navigations;

namespace OpenDictionary.ViewModels;

public sealed class WordGroupImportViewModel : ImportJsonViewModel<WordGroup>
{
    public WordGroupImportViewModel(IStorage<WordGroup> storage, INavigationService navigation, IToastMessageService toast)
        : base(storage, navigation, toast) { }
}