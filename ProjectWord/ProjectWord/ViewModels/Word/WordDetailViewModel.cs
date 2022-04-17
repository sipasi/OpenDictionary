#nullable enable

using System;
using System.Threading.Tasks;

using Framework.Words;
using Framework.Words.DictionarySources;

using OpenDictionary.Collections.Storages;
using OpenDictionary.Collections.Storages.Extensions;
using OpenDictionary.Models;
using OpenDictionary.Models.Extensions;
using OpenDictionary.Observables.Metadatas;
using OpenDictionary.Services.Audio;
using OpenDictionary.Services.Navigations;
using OpenDictionary.Views.Pages;

using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace OpenDictionary.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public class WordDetailViewModel : WordViewModel
    {
        private readonly INavigationService navigation;
        private readonly IDictionarySource source;
        private readonly IAudioPlayerServise audioPlayer;
        private readonly IPhoneticStorageService phoneticStorage;
        private readonly IStorage<WordMetadata> metadataStorage;

        private LayoutState metadataLoadState;
        private string? metadataCustomState;

        public LayoutState MetadataLoadState
        {
            get => metadataLoadState;
            set => SetProperty(ref metadataLoadState, value);
        }
        public string? MetadataCustomState
        {
            get => metadataCustomState;
            set => SetProperty(ref metadataCustomState, value);
        }

        public WordMetadataObservable Metadata { get; }

        public AsyncCommand RedirectToEditCommand { get; }
        public AsyncCommand LoadMetaDataCommand { get; }

        public AsyncCommand<string> PlayAudioCommand { get; }

        public WordDetailViewModel(
            IStorage<Word> wordStorage,
            IStorage<WordMetadata> metadataStorage,
            INavigationService navigation,
            IAudioPlayerServise audioPlayer,
            IPhoneticStorageService phoneticStorage,
            IDictionarySource source) : base(wordStorage, navigation)
        {
            this.metadataStorage = metadataStorage;
            this.source = source;
            this.navigation = navigation;
            this.audioPlayer = audioPlayer;
            this.phoneticStorage = phoneticStorage;

            Metadata = new WordMetadataObservable();
            MetadataLoadState = LayoutState.None;
            MetadataCustomState = null;

            RedirectToEditCommand = new AsyncCommand(RedirectToEdit);

            LoadMetaDataCommand = new AsyncCommand(LoadMetadata);

            PlayAudioCommand = new AsyncCommand<string>(PlayAudio);
        }

        protected override async ValueTask Load()
        {
            await base.Load();

            await LoadMetadata();
        }

        private async Task LoadMetadata()
        {
            if (string.IsNullOrWhiteSpace(Word.Origin))
            {
                return;
            }

            Metadata.Clear();

            MetadataLoadState = LayoutState.Loading;
            MetadataCustomState = null;

            try
            {
                IWordMetadata? metadata = await GetMetadataFrom(Word.Origin);

                Metadata.Set(metadata);
            }
            catch (Exception)
            {
                MetadataLoadState = LayoutState.Custom;

                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    MetadataCustomState = ErrorStates.NotFound;
                }
                else
                {
                    MetadataCustomState = ErrorStates.NoInternetConnection;
                }

                return;
            }

            MetadataLoadState = LayoutState.Success;
        }

        private async Task PlayAudio(string? path)
        {
            string? source = path;

            if (string.IsNullOrWhiteSpace(path) || string.IsNullOrWhiteSpace(Word.Origin))
            {
                return;
            }

            try
            {
                if (phoneticStorage.Contains(Word.Origin))
                {
                    source = phoneticStorage.GetFilePath(Word.Origin);
                }
                else
                {
                    bool added = await phoneticStorage.AddFromWebAsync(path, Word.Origin);

                    if (added)
                    {
                        source = phoneticStorage.GetFilePath(Word.Origin);
                    }
                }

                audioPlayer.Play(source);
            }
            catch (Exception e)
            {
                await ErrorMessage(e);
            }
        }

        private Task RedirectToEdit()
        {
            return navigation.GoToAsync<WordEditPage>(parameter: nameof(IEntity.Id), Id);
        }

        private async Task<IWordMetadata?> GetMetadataFrom(string word)
        {
            IWordMetadata? metadata = await metadataStorage
                .Query()
                .IncludeAll()
                .GetByWord(word);

            if (metadata == null)
            {
                metadata = await source.GetWord(word);

                if (metadata is null)
                {
                    return null;
                }

                WordMetadata clone = metadata.Clone();

                await metadataStorage.AddAsync(clone);
            }


            return metadata;
        }

        public void OnAppearing()
        {
            //LayoutState cacheCurrent = MetadataLoadState;
            //string? cacheCustom = MetadataCustomState;

            //MetadataLoadState = LayoutState.None;
            //MetadataCustomState = null;

            //MetadataLoadState = cacheCurrent;
            //MetadataCustomState = cacheCustom;
        }

        private class NoInternerException : Exception { }
        private class NotFoundException : Exception { }
    }
}