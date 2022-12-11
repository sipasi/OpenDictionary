using System.Text.Json;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Storage;

using OpenDictionary.Collections.Storages;
using OpenDictionary.Databases;
using OpenDictionary.DataTransfer;
using OpenDictionary.DataTransfer.Json;
using OpenDictionary.Maui.Services;
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
    public static IServiceCollection ConfigureOpenDictionary(this IServiceCollection services)
    {
        IFileExporter exporter = new JsonFileExporter()
        {
            CacheDirectory = FileSystem.CacheDirectory,
            Options = new JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                PropertyNamingPolicy = new WordGroupNamingPolicy()
            }
        };

        return services.ConfigureMauiServices()
            .ConfigureDataTransfer(exporter)
            .ConfigureAudio()
            .ConfigureDatabase()
            .ConfigureMessagesDialogs()
            .ConfigureIO()
            .ConfigureOnlineDictionary()
            .ConfigureViewModels()
            .ConfigureViews();
    }

    private static IServiceCollection ConfigureAudio(this IServiceCollection services)
    {
        services.AddSingleton<IPhoneticFilesService, PhoneticFilesService>();

        return services;
    }
    private static IServiceCollection ConfigureDatabase(this IServiceCollection services)
    {
        services
            .AddSingleton<IDatabasePath, UniversalDatabasePath>()
            .AddSingleton<IStorage<Word>, WordStorage>()
            .AddSingleton<IStorage<WordGroup>, WordGroupStorage>()
            .AddSingleton<IStorage<WordMetadata>, WordMetadataStorage>();

        return services;
    }
    private static IServiceCollection ConfigureMessagesDialogs(this IServiceCollection services)
    {
        services
            .AddSingleton<ILoadingMessageService, LoadingMessageService>()
            .AddSingleton<IDialogMessageService, PopupDialogMessageService>();

        return services;
    }
    private static IServiceCollection ConfigureIO(this IServiceCollection services)
    {
        IAppDirectoryService appDirectory = new AppDirectoryService(FileSystem.AppDataDirectory, FileSystem.CacheDirectory);

        services.AddSingleton(appDirectory);

        return services;
    }
    private static IServiceCollection ConfigureOnlineDictionary(this IServiceCollection services)
    {
        services
            .AddSingleton<IJsonParser, DictionaryApiJsonParser>()
            .AddSingleton<IDictionarySource, DictionaryApiRemoteSource>();

        return services;
    }
    private static IServiceCollection ConfigureViewModels(this IServiceCollection services)
    {
        services
            .AddTransient<WordDetailViewModel>()
            .AddTransient<WordEditViewModel>()

            .AddTransient<WordGroupDetailViewModel>()
            .AddTransient<WordGroupEditViewModel>()
            .AddTransient<WordGroupInfoList>()

            .AddTransient<GameListViewModel>()
            .AddTransient<OriginToTranslationViewModel>()
            .AddTransient<TranslationToOriginViewModel>()

            .AddTransient<SettingsViewModel>()
            .AddTransient<WordGroupExportViewModel>()
            .AddTransient<WordGroupImportViewModel>();

        return services;
    }
    private static IServiceCollection ConfigureViews(this IServiceCollection services)
    {
        services
            .AddTransient<Views.Pages.Settings.ExportPage>()
            .AddTransient<Views.Pages.Settings.ImportPage>()
            .AddTransient<Views.Pages.SettingsPage>()

            .AddTransient<Views.Pages.GameListPage>()
            .AddTransient<Views.Pages.GameOriginToTranslationPage>()
            .AddTransient<Views.Pages.GameTranslationToOriginPage>()

            .AddTransient<Views.Pages.WordDetailPage>()
            .AddTransient<Views.Pages.WordEditPage>()

            .AddTransient<Views.Pages.WordGroupCreatePage>()
            .AddTransient<Views.Pages.WordGroupDetailPage>()
            .AddTransient<Views.Pages.WordGroupListPage>();

        return services;
    }

    private sealed class WordGroupNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name) => name switch
        {
            nameof(FileData<object>.Datas) => nameof(WordGroup.Words),
            _ => name
        };
    }
}