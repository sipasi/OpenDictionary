using Framework.DependencyInjection;

using Microsoft.Maui.Controls;

using OpenDictionary.AppDatabase;
using OpenDictionary.Collections.Storages;
using OpenDictionary.Models;
using OpenDictionary.RemoteDictionaries.Parsers;
using OpenDictionary.RemoteDictionaries.Sources;
using OpenDictionary.Services.Audio;
using OpenDictionary.Services.Messages.Alerts;
using OpenDictionary.Services.Messages.Dialogs;
using OpenDictionary.Services.Messages.Toasts;
using OpenDictionary.Services.Navigations;
using OpenDictionary.ViewModels;
using OpenDictionary.ViewModels.Games;
using OpenDictionary.XamarinApp.Services.Messages.Alerts;
using OpenDictionary.XamarinApp.Services.Messages.Dialogs;
using OpenDictionary.XamarinApp.Services.Messages.Toasts;

namespace OpenDictionary.DependencyInjection.Extensions;

internal static class ServiceBuilderExtensions
{
    public static ServiceBuilder ConfigureAudio(this ServiceBuilder builder)
    {
        IAudioPlayerServise audioPlayer = DependencyService.Get<IAudioPlayerServise>();

        builder.singleton
            .Add<IPhoneticFilesService, PhoneticFilesService>();

#if ANDROID
        builder.singleton.Add<IAudioPlayerServise, Platforms.Android.AudioPlayer>();
#elif WINDOWS
        builder.singleton.Add<IAudioPlayerServise, Platforms.Windows.AudioPlayer>();
#else 
        throw new System.NotImplementedException("IAudioPlayerServise has not implement");
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
    public static ServiceBuilder ConfigureDialogs(this ServiceBuilder builder)
    {
        builder.singleton
            .Add<IAlertMessageService, AlertMessageService>()
            .Add<IDialogMessageService, DialogMessageService>();

        return builder;
    }
    public static ServiceBuilder ConfigureToastMessages(this ServiceBuilder builder)
    {
        builder.singleton
            .Add<IToastMessageService, ToastMessageService>();

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

            .Add<SettingsViewModel>();

        return builder;
    }
}