using System.Text.Json;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Storage;

using OpenDictionary.Databases;
using OpenDictionary.DataTransfer;
using OpenDictionary.DataTransfer.Json;
using OpenDictionary.ExternalAppTranslation;
using OpenDictionary.ExternalAppTranslation.GoogleServices;
using OpenDictionary.Maui.Services;
using OpenDictionary.Models;
using OpenDictionary.RemoteDictionaries.Parsers;
using OpenDictionary.RemoteDictionaries.Sources;
using OpenDictionary.Services.Audio;
using OpenDictionary.Services.ExternalAppTranslation;
using OpenDictionary.Services.IO;
using OpenDictionary.Services.Navigations;
using OpenDictionary.ViewModels;
using OpenDictionary.ViewModels.Games;
using OpenDictionary.ViewModels.Settings;
using OpenDictionary.ViewModels.WordGroups;
using OpenDictionary.ViewModels.Words;

namespace OpenDictionary.Services;

internal static class ServiceCollectionExtensions
{
    private sealed class WordGroupNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name) => name switch
        {
            nameof(FileData<object>.Data) => nameof(WordGroup.Words),
            _ => name
        };
    }

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

        services
            .ConfigureMauiServices()
            .ConfigureDataTransfer(exporter)
            .ConfigureAudio()
            .ConfigureDatabase()
            .ConfigureIO()
            .ConfigureOnlineDictionary()
            .ConfigureViewModels()
            .ConfigureViews()
            .ConfigureTranslator();

        services.Replace(new(typeof(INavigationService), typeof(NavigationService), ServiceLifetime.Singleton));

        services.AddSingleton<INavigationService, NavigationService>();

        return services;
    }

    private static IServiceCollection ConfigureAudio(this IServiceCollection services)
    {
        services.AddSingleton<IPhoneticFilesService, PhoneticFilesService>();

        return services;
    }
    private static IServiceCollection ConfigureDatabase(this IServiceCollection services)
    {
        services
            .AddSingleton<IDatabasePath>(DatabasePath.Create("open-dictionary-default"))
            .AddSingleton<IDatabaseConnection<AppDatabaseContext>, AppDatabaseConnection>()
            .AddSingleton<IDatabaseConnection<DatabaseContext>>(static provider => provider.GetService<IDatabaseConnection<AppDatabaseContext>>()!)
            .AddSingleton<IDatabaseConnection<DbContext>>(static provider => provider.GetService<IDatabaseConnection<AppDatabaseContext>>()!);

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
            .AddSingleton<IJsonParser, RemoteDictionaries.Parsers.Documents.DictionaryApiJsonParser>()
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

    private static IServiceCollection ConfigureTranslator(this IServiceCollection services)
    {
        services
            .AddSingleton<ILauncher>(Launcher.Default)
            .AddSingleton<IAppOrBrowserLauncher, AppOrBrowserLauncher>()
            .AddSingleton<IExternalTranslator, GoogleExternalTranslator>()
            .AddSingleton<ITranslatorUrlMaker, GoogleUriMaker>();

        return services;
    }
}