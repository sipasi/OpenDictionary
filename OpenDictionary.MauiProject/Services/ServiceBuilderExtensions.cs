using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Storage;

using OpenDictionary.Collections.Storages;
using OpenDictionary.Databases;
using OpenDictionary.MauiProject.Services.Messages.Dialogs;
using OpenDictionary.Models;
using OpenDictionary.RemoteDictionaries.Parsers;
using OpenDictionary.RemoteDictionaries.Sources;
using OpenDictionary.Services.Audio;
using OpenDictionary.Services.IO;
using OpenDictionary.Services.Messages.Dialogs;
using OpenDictionary.Services.Messages.Loadings;
using OpenDictionary.ViewModels;
using OpenDictionary.ViewModels.Games;
using OpenDictionary.ViewModels.Settings;

namespace OpenDictionary.Services;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureAudio(this IServiceCollection services)
    {
        services.AddSingleton<IPhoneticFilesService, PhoneticFilesService>();

        return services;
    }
    public static IServiceCollection ConfigureDatabase(this IServiceCollection services)
    {
        services
            .AddSingleton<IDatabasePath, UniversalDatabasePath>()
            .AddSingleton<IStorage<Word>, WordStorage>()
            .AddSingleton<IStorage<WordGroup>, WordGroupStorage>()
            .AddSingleton<IStorage<WordMetadata>, WordMetadataStorage>();

        return services;
    }
    public static IServiceCollection ConfigureMessagesDialogs(this IServiceCollection services)
    {
        services
            .AddSingleton<ILoadingMessageService, LoadingMessageService>()
            .AddSingleton<IDialogMessageService, PopupDialogMessageService>();

        return services;
    }
    public static IServiceCollection ConfigureIO(this IServiceCollection services)
    {
        IAppDirectoryService appDirectory = new AppDirectoryService(FileSystem.AppDataDirectory, FileSystem.CacheDirectory);

        services.AddSingleton(appDirectory);

        return services;
    }
    public static IServiceCollection ConfigureOnlineDictionary(this IServiceCollection services)
    {
        services
            .AddSingleton<IJsonParser, DictionaryApiJsonParser>()
            .AddSingleton<IDictionarySource, DictionaryApiRemoteSource>();

        return services;
    }
    public static IServiceCollection ConfigureViewModels(this IServiceCollection services)
    {
        services
            .AddSingleton<WordDetailViewModel>()
            .AddSingleton<WordEditViewModel>()

            .AddSingleton<WordGroupDetailViewModel>()
            .AddSingleton<WordGroupEditViewModel>()
            .AddSingleton<WordGroupInfoList>()

            .AddSingleton<GameListViewModel>()
            .AddSingleton<OriginToTranslationViewModel>()
            .AddSingleton<TranslationToOriginViewModel>()

            .AddSingleton<SettingsViewModel>()
            .AddSingleton<ExportViewModel>()
            .AddSingleton<ImportViewModel>();

        return services;
    }
    public static IServiceCollection ConfigureViews(this IServiceCollection services)
    {
        services
            .AddSingleton<OpenDictionary.Views.Pages.Settings.ExportPage>()
            .AddSingleton<OpenDictionary.Views.Pages.Settings.ImportPage>()
            .AddSingleton<OpenDictionary.Views.Pages.SettingsPage>()

            .AddSingleton<OpenDictionary.Views.Pages.GameListPage>()
            .AddSingleton<OpenDictionary.Views.Pages.GameOriginToTranslationPage>()
            .AddSingleton<OpenDictionary.Views.Pages.GameTranslationToOriginPage>()

            .AddSingleton<OpenDictionary.Views.Pages.WordDetailPage>()
            .AddSingleton<OpenDictionary.Views.Pages.WordEditPage>()

            .AddSingleton<OpenDictionary.Views.Pages.WordGroupCreatePage>()
            .AddSingleton<OpenDictionary.Views.Pages.WordGroupDetailPage>()
            .AddSingleton<OpenDictionary.Views.Pages.WordGroupListPage>();

        return services;
    }
}