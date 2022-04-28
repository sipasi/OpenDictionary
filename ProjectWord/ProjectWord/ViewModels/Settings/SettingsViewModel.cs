#nullable enable

using OpenDictionary.Collections.Storages;
using OpenDictionary.Models;
using OpenDictionary.Services.Dialogs;
using OpenDictionary.Services.ToastMessages;

namespace OpenDictionary.ViewModels
{
    public class SettingsViewModel : ViewModel
    {
        public AppThemeObservable AppTheme { get; }
        public ImportExportViewModel ImportExport { get; }
        public WordGroupDictionaryViewModel WordGroup { get; }

        public SettingsViewModel(IStorage<WordGroup> storage, IDialogWindowService dialog, IToastMessageService toast)
        {
            AppTheme = new AppThemeObservable();
            ImportExport = new ImportExportViewModel(storage, toast);
            WordGroup = new WordGroupDictionaryViewModel(storage, dialog, toast);
        }
    }
}