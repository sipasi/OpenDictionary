using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

using OpenDictionary.Collections.Storages;
using OpenDictionary.Databases;
using OpenDictionary.DataTransfer;
using OpenDictionary.DataTransfer.Json;
using OpenDictionary.Maui.Services;
using OpenDictionary.MauiProject;
using OpenDictionary.Models;
using OpenDictionary.RemoteDictionaries.Parsers;
using OpenDictionary.RemoteDictionaries.Sources;
using OpenDictionary.Services.Audio;
using OpenDictionary.Services.IO;
using OpenDictionary.Services.Navigations;
using OpenDictionary.ViewModels;
using OpenDictionary.ViewModels.Games;
using OpenDictionary.ViewModels.Settings;

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
    private sealed class NavigationService : INavigationService
    {
        public NavigationService()
        {
            Shell.Current.Navigating += Navigating;
        }

        public Task GoToAsync(string route) => Shell.Current.GoToAsync(route);
        public Task GoToAsync(string route, string parameter, string value) => Shell.Current.GoToAsync($"{route}?{parameter}={value}");
        public Task GoBackAsync() => Shell.Current.GoToAsync("..");

        private async void Navigating(object? sender, ShellNavigatingEventArgs e)
        {
            AppShell? shell = Shell.Current as AppShell;

            if (shell is null)
            {
                return;
            }

            if (e.CanCancel is false)
            {
                return;
            }

            if (e.Source is not ShellNavigationSource.Pop)
            {
                return;
            }


            if (shell.CurrentPage is Views.Pages.WordGroupListPage)
            { }
            if (shell.CurrentPage is Views.Pages.SettingsPage)
            {
                e.Cancel();

                shell.SetListPage();
            }
        }
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
            .ConfigureViews();

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
            .AddSingleton<IDatabasePath, UniversalDatabasePath>()
            .AddSingleton<IStorage<Word>, WordStorage>()
            .AddSingleton<IStorage<WordGroup>, WordGroupStorage>()
            .AddSingleton<IStorage<WordMetadata>, WordMetadataStorage>();

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
}