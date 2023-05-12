#nullable enable

using System;
using System.Threading.Tasks;

using CommunityToolkit.Maui.Converters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.Maui.Controls;
using Microsoft.Maui.Networking;


using OpenDictionary.Collections.Storages.Extensions;
using OpenDictionary.Databases;
using OpenDictionary.Models;
using OpenDictionary.RemoteDictionaries.Sources;
using OpenDictionary.Services.Audio;
using OpenDictionary.Services.Messages.Dialogs;
using OpenDictionary.Services.Messages.Toasts;
using OpenDictionary.Services.Navigations;
using OpenDictionary.Services.Navigations.Routes;
using OpenDictionary.ViewModels.Helpers;

namespace OpenDictionary.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public sealed partial class WordDetailViewModel : WordViewModel
{
    private readonly IDatabaseConnection<AppDatabaseContext> connection;
    private readonly INavigationService navigation;
    private readonly IDialogMessageService dialog;
    private readonly IAudioPlayerServise audioPlayer;
    private readonly IPhoneticFilesService phoneticFiles;
    private readonly IDictionarySource source;

    [ObservableProperty]
    private LayoutState metadataLoadState;
    [ObservableProperty]
    private string? metadataCustomState;

    public WordDetailViewModel(
        IDatabaseConnection<AppDatabaseContext> connection,
        INavigationService navigation,
        IDialogMessageService dialog,
        IToastMessageService toast,
        IAudioPlayerServise audioPlayer,
        IPhoneticFilesService phoneticFiles,
        IDictionarySource source) : base(connection, navigation, toast)
    {
        this.connection = connection;
        this.navigation = navigation;
        this.dialog = dialog;
        this.audioPlayer = audioPlayer;
        this.phoneticFiles = phoneticFiles;
        this.source = source;

        MetadataLoadState = LayoutState.None;
        MetadataCustomState = null;
    }

    protected override Task OnWordLoaded()
    {
        return LoadMetadata();
    }

    [RelayCommand]
    private async Task LoadMetadata()
    {
        if (string.IsNullOrWhiteSpace(Word.Origin))
        {
            return;
        }

        Metadata.Clear();

        MetadataLoadState = LayoutState.Loading;
        MetadataCustomState = null;

        WordMetadata? metadata = await GetMetadataFrom(Word.Origin);

        if (metadata is null)
        {
            MetadataLoadState = LayoutState.Custom;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                MetadataCustomState = ErrorStates.NotFound;
            }
            else if (Connectivity.NetworkAccess == NetworkAccess.None)
            {

                MetadataCustomState = ErrorStates.NoInternetConnection;
            }

            return;
        }

        Metadata.Set(metadata);

        MetadataLoadState = LayoutState.Success;
    }

    [RelayCommand]
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



            audioPlayer.Play(source!);
        }
        catch (Exception e)
        {
            await ErrorMessage(e);
        }
    }

    [RelayCommand]
    private async Task OnDelete()
    {
        await using AppDatabaseContext context = connection.Open();

        Guid id = Guid.Parse(Id);

        DialogResult result = await EntityDeleteDialog.Show(dialog);

        if (result is DialogResult.Ok)
        {
            Word group = (await context.Words.GetById(id))!;

            context.Remove(group);

            await context.SaveChangesAsync();

            await navigation.GoBackAsync();
        }
    }

    [RelayCommand]
    private Task RedirectToEdit()
    {
        return navigation.GoToAsync(AppRoutes.Word.Edit, parameter: nameof(IEntity.Id), Id);
    }

    private async Task<WordMetadata?> GetMetadataFrom(string word)
    {
        await using AppDatabaseContext context = connection.Open();

        WordMetadata? metadata = await context.WordMetadatas
            .IncludeAll()
            .GetByWord(word);

        if (metadata == null)
        {
            metadata = await source.GetWord(word);

            if (metadata is null)
            {
                return null;
            }

            await context.WordMetadatas.AddAsync(metadata);

            await context.SaveChangesAsync();
        }

        return metadata;
    }
}