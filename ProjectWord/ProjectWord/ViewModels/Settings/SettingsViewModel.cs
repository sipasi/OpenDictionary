#nullable enable

using OpenDictionary.Collections.Storages;
using OpenDictionary.Models;
using OpenDictionary.Services.ToastMessages;

namespace OpenDictionary.ViewModels
{
    public class SettingsViewModel : ViewModel
    {
        public AppThemeObservable AppTheme { get; }
        public ImportExportViewModel ImportExport { get; }

        public SettingsViewModel(IStorage<WordGroup> storage, IToastMessageService toast)
        {
            AppTheme = new AppThemeObservable();
            ImportExport = new ImportExportViewModel(storage, toast);
        }
    }
}