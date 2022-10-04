using Framework.DependencyInjection;

using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

using OpenDictionary.AppDatabase;
using OpenDictionary.Collections.Storages;
using OpenDictionary.MauiProject.Services.Messages.Alerts;
using OpenDictionary.MauiProject.Services.Messages.Dialogs;
using OpenDictionary.MauiProject.Services.Messages.Toasts;
using OpenDictionary.Models;
using OpenDictionary.RemoteDictionaries.Parsers;
using OpenDictionary.RemoteDictionaries.Sources;
using OpenDictionary.Services.Audio;
using OpenDictionary.Services.DataTransfer;
using OpenDictionary.Services.IO;
using OpenDictionary.Services.Messages.Alerts;
using OpenDictionary.Services.Messages.Dialogs;
using OpenDictionary.Services.Messages.Loadings;
using OpenDictionary.Services.Messages.Toasts;
using OpenDictionary.Services.Navigations;
using OpenDictionary.ViewModels;
using OpenDictionary.ViewModels.Games;
using OpenDictionary.ViewModels.Settings;

namespace OpenDictionary.DependencyInjection.Extensions;

internal static class ServiceBuilderExtensions
{
    public static ServiceBuilder ConfigureAudio(this ServiceBuilder builder)
    {
        IAudioPlayerServise audioPlayer = DependencyService.Get<IAudioPlayerServise>();

        builder.singleton
            .Add<IPhoneticFilesService, PhoneticFilesService>()
            .Add<IAudioPlayerServise, AudioPlayer>();

        return builder;
    }
    public static ServiceBuilder ConfigureDataTransfer(this ServiceBuilder builder)
    {
#if WINDOWS
        builder.singleton.Add<IFileExportService, DataShareServiceWindows>();
#else
        builder.singleton.Add<IFileExportService, DataShareService>();
#endif

        return builder;
    }
    public static ServiceBuilder ConfigureDatabase(this ServiceBuilder builder)
    {
        IDatabasePath path = new UniversalDatabasePath();

        builder.singleton
            .Add<IDatabasePath>(path)
            .Add<IStorage<Word>, WordStorage>()
            .Add<IStorage<WordGroup>, WordGroupStorage>()
            .Add<IStorage<WordMetadata>, WordMetadataStorage>();

        return builder;
    }
    public static ServiceBuilder ConfigureNavigation(this ServiceBuilder builder)
    {
        builder.singleton
            .Add<INavigationService, ShellNavigationService>();

        return builder;
    }
    public static ServiceBuilder ConfigureMessagesDialogs(this ServiceBuilder builder)
    {
        builder.singleton
            .Add<IAlertMessageService, AlertMessageService>()
            .Add<IToastMessageService, ToastMessageService>()
            .Add<ILoadingMessageService, LoadingMessageService>()
            .Add<IDialogMessageService, PopupDialogMessageService>();

        return builder;
    }
    public static ServiceBuilder ConfigureIO(this ServiceBuilder builder)
    {
        IAppDirectoryService appDirectory = new AppDirectoryService(FileSystem.AppDataDirectory, FileSystem.CacheDirectory);

        builder.singleton.Add<IAppDirectoryService>(appDirectory);

        return builder;
    }
    public static ServiceBuilder ConfigureOnlineDictionary(this ServiceBuilder builder)
    {
        builder.singleton
            .Add<IJsonParser, DictionaryApiJsonParser>()
            .Add<IDictionarySource, DictionaryApiRemoteSource>();

        return builder;
    }
    public static ServiceBuilder ConfigureViewModels(this ServiceBuilder builder)
    {
        builder.transient
            .Add<WordDetailViewModel>()
            .Add<WordEditViewModel>()

            .Add<WordGroupDetailViewModel>()
            .Add<WordGroupEditViewModel>()
            .Add<WordGroupInfoList>()

            .Add<GameListViewModel>()
            .Add<OriginToTranslationViewModel>()
            .Add<TranslationToOriginViewModel>()

            .Add<SettingsViewModel>()
            .Add<ExportViewModel>()
            .Add<ImportViewModel>();

        return builder;
    }
}