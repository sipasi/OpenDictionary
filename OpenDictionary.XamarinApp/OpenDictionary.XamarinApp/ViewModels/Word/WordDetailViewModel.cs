#nullable enable

using System;
using System.Threading.Tasks;

using OpenDictionary.Collections.Storages;
using OpenDictionary.Collections.Storages.Extensions;
using OpenDictionary.Models;
using OpenDictionary.Observables.Metadatas;
using OpenDictionary.RemoteDictionaries.Sources;
using OpenDictionary.Services.Audio;
using OpenDictionary.Services.Messages.Dialogs;
using OpenDictionary.Services.Navigations;
using OpenDictionary.Services.Navigations.Routes;
using OpenDictionary.ViewModels.Helpers;

using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace OpenDictionary.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public class WordDetailViewModel : WordViewModel
    {
        private readonly IStorage<Word> wordStorage;
        private readonly IStorage<WordMetadata> metadataStorage;
        private readonly INavigationService navigation;
        private readonly IDialogMessageService dialog;
        private readonly IAudioPlayerServise audioPlayer;
        private readonly IPhoneticFilesService phoneticFiles;
        private readonly IDictionarySource source;

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

        public AsyncCommand DeleteCommand { get; }

        public AsyncCommand RedirectToEditCommand { get; }
        public AsyncCommand LoadMetaDataCommand { get; }

        public AsyncCommand<string> PlayAudioCommand { get; }

        public WordDetailViewModel(
            IStorage<Word> wordStorage,
            IStorage<WordMetadata> metadataStorage,
            INavigationService navigation,
            IDialogMessageService dialog,
            IAudioPlayerServise audioPlayer,
            IPhoneticFilesService phoneticStorage,
            IDictionarySource source) : base(wordStorage, navigation)
        {
            this.wordStorage = wordStorage;
            this.metadataStorage = metadataStorage;
            this.navigation = navigation;
            this.dialog = dialog;
            this.audioPlayer = audioPlayer;
            this.phoneticFiles = phoneticStorage;
            this.source = source;

            Metadata = new WordMetadataObservable();
            MetadataLoadState = LayoutState.None;
            MetadataCustomState = null;

            DeleteCommand = new AsyncCommand(OnDelete);

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
                WordMetadata? metadata = await GetMetadataFrom(Word.Origin);

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
                if (phoneticFiles.Contains(path, Word.Origin))
                {
                    source = phoneticFiles.GetFilePath(path, Word.Origin);
                }
                else
                {
                    bool added = await phoneticFiles.AddFromWebAsync(path, Word.Origin);

                    if (added)
                    {
                        source = phoneticFiles.GetFilePath(path, Word.Origin);
                    }
                }

                audioPlayer.Play(source);
            }
            catch (Exception e)
            {
                await ErrorMessage(e);
            }
        }

        private async Task OnDelete()
        {
            IStorage<Word> storage = wordStorage;

            Guid id = Guid.Parse(Id);

            DialogResult result = await EntityDeleteDialog.Show(dialog);

            if (result is DialogResult.Ok)
            {
                Word group = await storage.Query().GetById(id);

                await storage.DeleteAsync(group);

                await navigation.GoBackAsync();
            }
        }

        private Task RedirectToEdit()
        {
            return navigation.GoToAsync(AppRoutes.Word.Edit, parameter: nameof(IEntity.Id), Id);
        }

        private async Task<WordMetadata?> GetMetadataFrom(string word)
        {
            WordMetadata? metadata = await metadataStorage
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

                await metadataStorage.AddAsync(metadata);
            }

            return metadata;
        }
    }
}