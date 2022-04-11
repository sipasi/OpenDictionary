#nullable enable

using System;
using System.Threading.Tasks;

using Framework.Words;
using Framework.Words.DictionarySources;
using Framework.Words.Parsers;

using ProjectWord.Collections.Storages;
using ProjectWord.Collections.Storages.Extensions;
using ProjectWord.Models;
using ProjectWord.Observables.Words;
using ProjectWord.Services.Audio;
using ProjectWord.Services.Collections;

using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace ProjectWord.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public class WordEditViewModel : ViewModel
    {
        private string id;
        private string origin;
        private string translation;

        private bool metadataLoading;
        private bool metadataLoadingComplete;

        private readonly IStorage<Word> wordStorage = StorageService<Word>.Storage;

        public string Id
        {
            get => id;
            set
            {
                id = value;

                LoadWordDataCommand.ExecuteAsync();
            }
        }

        public bool IsNew => string.IsNullOrWhiteSpace(id);

        public string Origin { get => origin; set => SetProperty(ref origin, value); }
        public string Translation { get => translation; set => SetProperty(ref translation, value); }

        public bool MetadataLoading { get => metadataLoading; set => SetProperty(ref metadataLoading, value); }
        public bool MetadataLoadingComplete { get => metadataLoadingComplete; set => SetProperty(ref metadataLoadingComplete, value); }

        public WordMetadataObservable Metadata { get; }

        public AsyncCommand LoadMetaDataCommand { get; }
        public AsyncCommand LoadWordDataCommand { get; }
        public AsyncCommand SaveCommand { get; }
        public Command DiscardCommand { get; }

        public AsyncCommand<string> PlayAudioCommand { get; }

        public WordEditViewModel()
        {
            id = string.Empty;
            origin = string.Empty;
            translation = string.Empty;

            LoadWordDataCommand = new AsyncCommand(LoadWord);
            LoadMetaDataCommand = new AsyncCommand(LoadMetadata);

            SaveCommand = new AsyncCommand(OnSave);
            DiscardCommand = new Command(OnDiscard);

            PlayAudioCommand = new AsyncCommand<string>(PlayAudio);

            Metadata = new WordMetadataObservable();
            MetadataLoading = false;
        }

        private async Task OnSave()
        {
            Guid guid = Guid.Parse(id);

            Word word = await wordStorage.Query().GetById(guid);

            word.Origin = origin;
            word.Translation = translation;

            await wordStorage.UpdateAsync(word);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
        private async void OnDiscard(object obj)
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async Task LoadWord()
        {
            try
            {
                if (IsNew)
                {
                    throw new NotSupportedException();
                }

                Guid guid = Guid.Parse(id);

                Word word = await wordStorage.Query().GetById(guid);

                Origin = word!.Origin;
                Translation = word!.Translation;
            }
            catch (Exception e)
            {
                await ErrorMessage(e);

                await Shell.Current.GoToAsync("..");
            }
        }
        private async Task LoadMetadata()
        {
            if (string.IsNullOrWhiteSpace(Origin))
            {
                return;
            }

            try
            {
                Metadata.Clear();

                MetadataLoading = true;
                MetadataLoadingComplete = false;

                await Task.Delay(1000);

                IWordMetadata? metadata = await GetMetadata(Origin);

                Metadata.Set(metadata);
            }
            catch (Exception e)
            {
                await ErrorMessage(e);
            }
            finally
            {
                MetadataLoading = false;

                MetadataLoadingComplete = true;
            }
        }

        private async Task PlayAudio(string? path)
        {
            try
            {
                IAudioServise audio = DependencyService.Get<IAudioServise>();

                audio.Play(path);
            }
            catch (Exception e)
            {
                await ErrorMessage(e);
            }
        }

        private static Task<IWordMetadata?> GetMetadata(string word)
        {
            DictionaryApiJsonParser parser = new DictionaryApiJsonParser();
            DictionaryApiRemoteSource source = new DictionaryApiRemoteSource(parser);

            return source.GetWord(word);
        }

        private static Task ErrorMessage(Exception exception)
        {
            string title = nameof(WordEditViewModel);
            string message = exception.Message;
            string cancel = "Close";

            return Shell.Current.DisplayAlert(title, message, cancel);
        }
    }
}