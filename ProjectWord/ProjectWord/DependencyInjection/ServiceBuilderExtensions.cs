﻿using Framework.DependencyInjection;
using Framework.Words.DictionarySources;
using Framework.Words.Parsers;

using OpenDictionary.AppDatabase;
using OpenDictionary.Collections.Storages;
using OpenDictionary.Models;
using OpenDictionary.Services.Audio;
using OpenDictionary.Services.Navigations;
using OpenDictionary.ViewModels;
using OpenDictionary.ViewModels.Games;

using Xamarin.Forms;

namespace OpenDictionary.DependencyInjection.Extensions
{
    internal static class ServiceBuilderExtensions
    {
        public static ServiceBuilder ConfigureAudio(this ServiceBuilder builder)
        {
            IAudioPlayerServise audioPlayer = DependencyService.Get<IAudioPlayerServise>();

            builder.singleton
                .Add<IPhoneticStorageService, PhoneticStorageService>()
                .Add<IAudioPlayerServise>(audioPlayer);

            return builder;
        }
        public static ServiceBuilder ConfigureDatabase(this ServiceBuilder builder)
        {
            IDatabasePath path = DependencyService.Get<IDatabasePath>();

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
                .Add<WordGroupListViewModel>()

                .Add<GameListViewModel>()
                .Add<OriginToTranslationViewModel>()
                .Add<TranslationToOriginViewModel>()

                .Add<SettingsViewModel>();

            return builder;
        }
    }
}
