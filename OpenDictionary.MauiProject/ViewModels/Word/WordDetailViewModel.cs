#nullable enable

using System;
using System.Threading.Tasks;

using CommunityToolkit.Maui.Converters;

using Framework.Words;
using Framework.Words.DictionarySources;

using Microsoft.Maui.Controls;
using Microsoft.Maui.Networking;

using MvvmHelpers.Commands;

using OpenDictionary.Collections.Storages;
using OpenDictionary.Collections.Storages.Extensions;
using OpenDictionary.Models;
using OpenDictionary.Models.Extensions;
using OpenDictionary.Observables.Metadatas;
using OpenDictionary.Services.Audio;
using OpenDictionary.Services.Messages.Dialogs;
using OpenDictionary.Services.Navigations;
using OpenDictionary.ViewModels.Helpers;
using OpenDictionary.Views.Pages;

namespace OpenDictionary.ViewModels;

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
            IWordMetadata? metadata = await GetMetadataFrom(Word.Origin) ?? throw new Exception();

            Metadata.Set(metadata);
        }
        catch (Exception e)
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
}